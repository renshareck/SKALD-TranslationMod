# 中文翻译说明
## 说明
* 使用AI进行翻译，未进行任何人工矫正，望后来者可对其进行矫正优化
* 异色异底色等字体未进行修改，和正常字体一样

## 脚本说明
### translate.py
读取for_translations目录下待翻译的csv文件，通过硅基流动API进行翻译，翻译csv保存至translations目录

### GetChineseString.py
读取translations目录已翻译的csv文件中translate列文本，将其中字符进行去重排序

### font_atlas_generator.py 
通过AI经韩语包中fontMaker脚本修改而来(python == 3.8)

需要将GetChineseString.py中获取到的字符串赋给RAW_DATA变量后再运行

最后生成的内容需要在末尾手动添加0-89号文本（或者在RAW_DATA前按照已有顺序添加也行）

常用参数如下

|fonts类型|单元格宽高度|图集宽度|x偏移|y偏移|字体大小|
|:-------|:---------|:------|:---|:---|:-----|
|tiny|8|72|0|1|8|
|big|17|153|2|2|12|
|large|21|189|4|2|12|
|huge|21|189|4|2|12|

当前huge、large、big使用字体为 [Fusion Pixel Font 12像素](https://github.com/TakWolf/fusion-pixel-font) ， tiny使用字体为 [MisekiBitmap](https://github.com/ItMarki/MisekiBitmap) 。