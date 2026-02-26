#!/usr/bin/env python3
"""
图片拼接脚本 - 将head和tail目录中的同名图片进行拼接
用法: python image_stitch.py [--head HEAD_DIR] [--tail TAIL_DIR] [--output OUTPUT_DIR]
"""

import os
import sys
from pathlib import Path
from PIL import Image

def get_image_files(directory: Path) -> set:
    """获取目录中所有图片文件"""
    image_extensions = {'.jpg', '.jpeg', '.png', '.bmp', '.gif', '.tiff', '.webp'}
    return {
        f.name for f in directory.iterdir()
        if f.is_file() and f.suffix.lower() in image_extensions
    }


def stitch_images(head_path: Path, tail_path: Path, output_path: Path):
    """
    拼接两张图片:
    1. 检查 head宽度 == tail宽度
    4. 将tail图片拼接到扩展后的head图像右侧
    """
    with Image.open(head_path).convert("RGBA") as head_img:
        with Image.open(tail_path).convert("RGBA") as tail_img:
            head_width, head_height = head_img.size
            tail_width, tail_height = tail_img.size

            flag1 = (head_width - 1 != tail_width)
            flag0 = (head_width != tail_width)
            if flag0 & flag1:
                print(f"宽度不一致: head={head_width}, tail={tail_width}")
                return False

            # 新宽度 = tail宽度 + 1
            new_width = tail_width
            if not flag1:
                new_width = tail_width + 1
            # 新高度 = head高度
            
            new_height = head_height + tail_height

            stitched = Image.new("RGBA", (new_width, new_height), (0, 0, 0, 0))

            # 粘贴head图片
            stitched.paste(head_img, (0, 0))

            # 粘贴tail图片到head下侧
            stitched.paste(tail_img, (0, head_height))

            stitched.save(output_path, "PNG", compress_level=0)
            return True, f"成功保存: {output_path}"


def main():
    import argparse
    parser = argparse.ArgumentParser(description='图片拼接工具')
    parser.add_argument('--head', default='D:\\project\\MachineLearning\\SKLADTranslation\\test_fonts\\big_ext', help='head图片目录 (默认: head)')
    parser.add_argument('--tail', default='D:\\project\\MachineLearning\\SKLADTranslation\\test_fonts\\big_90', help='tail图片目录 (默认: tail)')
    parser.add_argument('--output', default='D:\\project\\MachineLearning\\SKLADTranslation\\test_fonts\\big_output', help='输出目录 (默认: output)')
    args = parser.parse_args()

    head_dir = Path(args.head)
    tail_dir = Path(args.tail)
    output_dir = Path(args.output)

    # 检查目录是否存在
    if not head_dir.exists():
        print(f"错误: head目录不存在: {head_dir}")
        sys.exit(1)
    if not tail_dir.exists():
        print(f"错误: tail目录不存在: {tail_dir}")
        sys.exit(1)

    # 创建输出目录
    output_dir.mkdir(parents=True, exist_ok=True)

    # 获取图片文件
    head_files = get_image_files(head_dir)
    tail_files = get_image_files(tail_dir)

    # 找出同名文件
    common_files = head_files & tail_files

    if not common_files:
        print("错误: head和tail目录中没有找到同名图片文件")
        sys.exit(1)

    print(f"找到 {len(common_files)} 个同名图片文件\n")

    success_count = 0
    error_count = 0

    for filename in sorted(common_files):
        head_path = head_dir / filename
        tail_path = tail_dir / filename
        output_path = output_dir / filename

        with Image.open(head_path) as head_img:
            with Image.open(tail_path) as tail_img:
                head_width = head_img.width
                tail_width = tail_img.width

                flag1 = (head_width - 1 != tail_width)
                flag0 = (head_width != tail_width)
                if flag0 & flag1:
                    print(f"宽度不一致: head={head_width}, tail={tail_width}")
                    error_count += 1
                else:
                    success, message = stitch_images(head_path, tail_path, output_path)
                    if success:
                        print(f"✅ {filename}: {message}")
                        success_count += 1
                    else:
                        print(f"❌ {filename}: {message}")
                        error_count += 1

    print(f"\n完成: 成功 {success_count} 个, 失败 {error_count} 个")

if __name__ == '__main__':
    main()