#!/usr/bin/env python3
"""
DND游戏文本翻译脚本 - 使用硅基流动API
功能：翻译/home/agent/translations目录下的CSV文件，保留花括号占位符，符合DND风格
"""

import os
import csv
import re
import json
import time
from pathlib import Path
from typing import Optional
import traceback

# ============ 配置 ============
SILICONFLOW_API_KEY = os.environ.get("SILICONFLOW_API_KEY", "your-api-key-here")
SILICONFLOW_BASE_URL = "https://api.siliconflow.cn/v1/"
TRANSLATIONS_DIR = "for_translations"  # 待翻译文本放置到此目录下
OUTPUT_DIR = "translations"
MODEL = "Pro/Qwen/Qwen2.5-7B-Instruct"


# ============ 系统提示词 ============
SYSTEM_PROMPT = """你是一个专业的DND（龙与地下城）游戏本地化翻译专家。

翻译要求：
1. 只翻译内容，保留所有花括号占位符（如 {PLAYER}、{ITEM}、{VALUE} 等）原样不动
2. 使用符合DND背景的中文术语，保持风格一致
3. 专有名词必须统一翻译（见下方对照表）
4. 保持游戏术语的专业性和准确性
5. 翻译要自然流畅，符合中文表达习惯

专有名词统一表：
Barbarian: 野蛮人
Bard: 吟游诗人
Cleric: 牧师
Druid: 德鲁伊
Fighter: 战士
Monk: 武僧
Paladin: 圣武士
Ranger: 游侠
Rogue: 盗贼
Sorcerer: 术士
Warlock: 术士
Wizard: 法师
Human: 人类
Elf: 精灵
Dwarf: 矮人
Halfling: 半身人
Gnome: 侏儒
Half-Orc: 半兽人
Tiefling: 提夫林
Dragonborn: 龙裔
Goblin: 地精
Orc: 兽人
Skeleton: 骷髅
Zombie: 僵尸
Vampire: 吸血鬼
Werewolf: 狼人
Dragon: 龙
Beholder: 眼魔
Mind Flayer: 夺心魔
Lich: 巫妖
Dungeon: 地下城
Tavern: 酒馆
Temple: 神殿
Village: 村庄
Kingdom: 王国
Quest: 任务
Adventure: 冒险
Party: 队伍

规则：
- 如果原文已被翻译过（translate列有内容且不是英文），跳过不翻译
- 如果原文包含花括号，翻译时必须保留
- 保持原文的格式和标点
- 游戏机制类词汇要使用标准DND中文术语
"""


def extract_placeholders(text: str) -> tuple[list[tuple[str, str]], str]:
    """提取花括号占位符并替换为临时标记"""
    placeholders = []
    pattern = r'\{[^}]+\}'
    
    def replacer(match):
        placeholder = match.group()
        marker = f"__PLACEHOLDER_{len(placeholders)}__"
        placeholders.append((marker, placeholder))
        return marker
    
    protected_text = re.sub(pattern, replacer, text)
    return placeholders, protected_text


def restore_placeholders(text: str, placeholders: list[tuple[str, str]]) -> str:
    """恢复花括号占位符"""
    result = text
    for marker, original in placeholders:
        result = result.replace(marker, original)
    return result


def load_processed_translations() -> dict:
    """加载已处理过的翻译记录，防止重复翻译"""
    cache_file = Path("D:\\project\\MachineLearning\\SKLADTranslation\\translation_cache.json")
    if cache_file.exists():
        try:
            with open(cache_file, 'r', encoding='utf-8') as f:
                return json.load(f)
        except:
            pass
    return {}


def save_processed_translations(cache: dict):
    """保存翻译缓存"""
    cache_file = Path("D:\\project\\MachineLearning\\SKLADTranslation\\translation_cache.json")
    cache_file.parent.mkdir(parents=True, exist_ok=True)
    with open(cache_file, 'w', encoding='utf-8') as f:
        json.dump(cache, f, ensure_ascii=False, indent=2)


def translate_text_batch(texts: list[str], api_key: str) -> Optional[list[str]]:
    """批量翻译文本"""
    if not texts:
        return None
    
    import requests
    
    headers = {
        "Authorization": f"Bearer {api_key}",
        "Content-Type": "application/json"
    }
    
    # 构建批量翻译的prompt
    user_content = "请翻译以下DND游戏文本列表，按顺序返回翻译结果（每行一条）：\n\n"
    for i, text in enumerate(texts):
        user_content += f"{i+1}. {text}\n"
    
    payload = {
        "model": MODEL,
        "messages": [
            {"role": "system", "content": SYSTEM_PROMPT},
            {"role": "user", "content": user_content}
        ],
        "temperature": 0.3,
        "max_tokens": 4000
    }
    
    try:
        response = requests.post(
            f"{SILICONFLOW_BASE_URL}/chat/completions",
            headers=headers,
            json=payload,
            timeout=120
        )
        response.raise_for_status()
        
        result = response.json()
        content = result["choices"][0]["message"]["content"]
        
        # 解析返回的翻译结果
        translations = []
        lines = content.strip().split('\n')
        for line in lines:
            line = line.strip()
            if not line:
                continue
            # 移除行号（如 "1. " 或 "1. " 开头）
            line = re.sub(r'^\d+[.)]\s*', '', line)
            translations.append(line)
        
        # 确保数量匹配
        while len(translations) < len(texts):
            translations.append(translations[-1] if translations else texts[-1])
        
        return translations[:len(texts)]
        
    except Exception as e:
        print(f"API调用错误: {e}")
        return None


def translate_single_text(text: str, api_key: str) -> Optional[str]:
    """单条文本翻译（备用方法）"""
    import requests
    
    headers = {
        "Authorization": f"Bearer {api_key}",
        "Content-Type": "application/json"
    }

    
    payload = {
        "model": MODEL,
        "messages": [
            {"role": "system", "content": SYSTEM_PROMPT},
            {"role": "user", "content": f"翻译此DND文本：\n\n{text}"}
        ],
        "temperature": 0.3,
        "max_tokens": 1000
    }
    
    try:
        response = requests.post(
            f"{SILICONFLOW_BASE_URL}/chat/completions",
            headers=headers,
            json=payload,
            timeout=60
        )
        response.raise_for_status()
        result = response.json()
        return result["choices"][0]["message"]["content"]
    except Exception as e:
        print(f"翻译错误: {e}")
        return None


def process_csv_file(input_path: Path, output_path: Path, api_key: str):
    """处理单个CSV文件"""
    print(f"处理文件: {input_path.name}")
    
    # 读取CSV
    rows = []
    with open(input_path, 'r', encoding='utf-8') as f:
        reader = csv.DictReader(f)
        fieldnames = reader.fieldnames
        for row in reader:
            rows.append(row)
    
    # 待翻译的行
    rows_to_translate = []
    indices_to_translate = []
    
    for i, row in enumerate(rows):
        original = row.get('original', '').strip()
        translate = row.get('translate', '').strip()
        
        # 跳过空行、已翻译的行、纯英文但translate列已有内容
        # if not original:
        #    continue
        # if translate and is_likely_chinese(translate):
        #     continue
        # if translate == original and not has_placeholder(original):
        #    continue
            
        rows_to_translate.append(original)
        indices_to_translate.append(i)
    
    # 批量翻译（每批20条）
    translations_result = {}
    batch_size = 20
    
    for batch_start in range(0, len(rows_to_translate), batch_size):
        batch_end = min(batch_start + batch_size, len(rows_to_translate))
        batch = rows_to_translate[batch_start:batch_end]
        batch_indices = indices_to_translate[batch_start:batch_end]
        
        print(f"  翻译批次 {batch_start//batch_size + 1}: {len(batch)} 条...")
        
        # 替换占位符
        protected_batch = []
        all_placeholders = []
        for text in batch:
            placeholders, protected = extract_placeholders(text)
            protected_batch.append(protected)
            all_placeholders.append(placeholders)
        
        # 批量翻译
        translated_batch = translate_text_batch(protected_batch, api_key)
        
        if translated_batch:
            # 恢复占位符
            for i, translation in enumerate(translated_batch):
                original_idx = batch_indices[protected_batch.index(protected_batch[i]) if i < len(protected_batch) else 0]
                # 找到正确的映射
                for j, orig_idx in enumerate(batch_indices):
                    if batch_start + j < len(all_placeholders):
                        translation = restore_placeholders(translation, all_placeholders[j])
                translations_result[original_idx] = translation
        else:
            # 降级为单条翻译
            print("  批量翻译失败，降级为单条翻译...")
            for i, text in enumerate(batch):
                original_idx = batch_indices[i]
                translation = translate_single_text(text, api_key)
                if translation:
                    translation = restore_placeholders(translation, all_placeholders[i])
                    translations_result[original_idx] = translation
                else:
                    translations_result[original_idx] = text  # 使用原文
        
        time.sleep(0.5)  # 避免API限流
    
    # 更新行数据
    for idx, translation in translations_result.items():
        rows[idx]['translate'] = translation
    
    # 写入输出文件
    with open(output_path, 'w', encoding='utf-8', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(rows)
    
    print(f"  完成: {len(rows_to_translate)} 条待翻译，{len(translations_result)} 条已翻译")
    return len(translations_result)


def has_placeholder(text: str) -> bool:
    """检查是否包含花括号占位符"""
    return bool(re.search(r'\{[^}]+\}', text))


def is_likely_chinese(text: str) -> bool:
    """判断文本是否可能是中文"""
    chinese_chars = sum(1 for c in text if '\u4e00' <= c <= '\u9fff')
    return chinese_chars > len(text) * 0.3


def main():
    """主函数"""
    print("=" * 60)
    print("DND游戏文本翻译器 - 硅基流动API版本")
    print("=" * 60)
    
    # 检查API Key
    if SILICONFLOW_API_KEY == "your-api-key-here":
        print("错误：请设置 SILICONFLOW_API_KEY 环境变量")
        print("示例：export SILICONFLOW_API_KEY='your-api-key'")
        return
    
    # 确保输出目录存在
    Path(OUTPUT_DIR).mkdir(parents=True, exist_ok=True)
    
    # 加载翻译缓存
    translation_cache = load_processed_translations()
    
    # 获取CSV文件列表
    translations_path = Path(TRANSLATIONS_DIR)
    csv_files = []
    csv_files += list(translations_path.glob("*.CSV"))
    
    print(f"找到 {len(csv_files)} 个CSV文件")
    print(f"源目录: {TRANSLATIONS_DIR}")
    print(f"输出目录: {OUTPUT_DIR}")
    print()
    
    total_translated = 0
    
    for csv_file in sorted(csv_files):
        if csv_file.name.startswith('.'):
            continue
        
        output_path = Path(OUTPUT_DIR) / csv_file.name
        
        try:
            count = process_csv_file(csv_file, output_path, SILICONFLOW_API_KEY)
            total_translated += count
            print()
        except Exception:
            err_str = traceback.format_exc()
            print(f"处理文件 {csv_file.name} 时出错:\n {err_str}")
            print()
    
    print("=" * 60)
    print(f"翻译完成！共翻译 {total_translated} 条文本")
    print(f"输出文件保存在: {OUTPUT_DIR}")
    print("=" * 60)


if __name__ == "__main__":
    main()