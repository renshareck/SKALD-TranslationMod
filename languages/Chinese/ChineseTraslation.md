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


|font                           |celll H/W  |font size  |X offset   |Y offset   |color              |ttf    |
|:------------------------------|:----------|:----------|:----------|:----------|:------------------|:------|
|BigFont.png                    |16         |16         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       |    
|IlluminatedFont.png            |16         |16         |0          |0          |rgb(237, 241, 113) |WenQuanYi.Bitmap.Song.16px.ttf       |
|IlluminatedFontLarge.png       |20         |20         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       |   
|InsularHuge.png                |20         |20         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       |
|InsularMedium.png              |16         |16         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       |  
|InsularTiny.png                |10         |10         |0          |0          |rgb(0, 0, 0)       |fusion-pixel-10px-proportional-zh_hans.ttf       |
|Logo.png                       |           |           |           |           |                   |fusion-pixel-10px-proportional-zh_hans.ttf       |
|MedievalHuge.png               |28         |24         |0          |-4         |rgb(237, 241, 113) |WenQuanYi.Bitmap.Song.16px.ttf       |
|MedievalHugeThin.png           |28         |24         |0          |-4         |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       |
|MedievalMedium.png             |20         |20         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       | 
|MediumFont.png                 |16         |16         |0          |0          |rgb(0, 0, 0)       |WenQuanYi.Bitmap.Song.16px.ttf       | 
|MediumFontBlue.png             |16         |16         |0          |0          |rgb(117, 206, 200) |WenQuanYi.Bitmap.Song.16px.ttf       | 
|TinyFont.png                   |10         |10         |0          |0          |rgb(178, 178, 178) |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontCapitalized.png        |10         |10         |0          |0          |rgb(178, 178, 178) |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontCapitalizedYellow.png  |10         |10         |0          |0          |rgb(237, 241, 113) |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontFat.png                |10         |10         |0          |0          |rgb(0, 0, 0)       |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontTall.png               |10         |10         |0          |0          |rgb(237, 241, 113) |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontTallCapitalized.png    |10         |10         |0          |0          |rgb(237, 241, 113) |fusion-pixel-10px-proportional-zh_hans.ttf       |
|TinyFontYellow.png             |10         |10         |0          |0          |rgb(237, 241, 113) |fusion-pixel-10px-proportional-zh_hans.ttf       |