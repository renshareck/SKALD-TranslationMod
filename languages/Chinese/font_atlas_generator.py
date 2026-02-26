# -*- coding: utf-8 -*-
# font_atlas_generator_cn.py

import json
import math
import os
import sys
from typing import Optional, Tuple
import tkinter as tk
from tkinter import ttk, filedialog, colorchooser, messagebox

from PIL import Image, ImageTk, ImageFont, ImageDraw


# ========= 1) 字符集：把你 C# 的 rawData 粘贴到这里 =========
#RAW_DATA = """ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz!0123456789.,:/%-+?'\"()=*_[]#<>;{}|  가각간갇갈갉갊감갑값갓갔강갖갗같갚갛개객갠갤갬갭갯갰갱갸갹갼걀걋걍걔걘걜거걱건걷걸걺검겁것겄겅겆겉겊겋게겐겔겜겝겟겠겡겨격겪견겯결겸겹겻겼경곁계곈곌곕곗고곡곤곧골곪곬곯곰곱곳공곶과곽관괄괆괌괍괏광괘괜괠괩괬괭괴괵괸괼굄굅굇굉교굔굘굡굣구국군굳굴굵굶굻굼굽굿궁궂궈궉권궐궜궝궤궷귀귁귄귈귐귑귓규균귤그극근귿글긁금급긋긍긔기긱긴긷길긺김깁깃깅깆깊까깍깎깐깔깖깜깝깟깠깡깥깨깩깬깰깸깹깻깼깽꺄꺅꺌꺼꺽꺾껀껄껌껍껏껐껑께껙껜껨껫껭껴껸껼꼇꼈꼍꼐꼬꼭꼰꼲꼴꼼꼽꼿꽁꽂꽃꽈꽉꽐꽜꽝꽤꽥꽹꾀꾄꾈꾐꾑꾕꾜꾸꾹꾼꿀꿇꿈꿉꿋꿍꿎꿔꿜꿨꿩꿰꿱꿴꿸뀀뀁뀄뀌뀐뀔뀜뀝뀨끄끅끈끊끌끎끓끔끕끗끙끝끼끽낀낄낌낍낏낑나낙낚난낟날낡낢남납낫났낭낮낯낱낳내낵낸낼냄냅냇냈냉냐냑냔냘냠냥너넉넋넌널넒넓넘넙넛넜넝넣네넥넨넬넴넵넷넸넹녀녁년녈념녑녔녕녘녜녠노녹논놀놂놈놉놋농높놓놔놘놜놨뇌뇐뇔뇜뇝뇟뇨뇩뇬뇰뇹뇻뇽누눅눈눋눌눔눕눗눙눠눴눼뉘뉜뉠뉨뉩뉴뉵뉼늄늅늉느늑는늘늙늚늠늡늣능늦늪늬늰늴니닉닌닐닒님닙닛닝닢다닥닦단닫달닭닮닯닳담답닷닸당닺닻닿대댁댄댈댐댑댓댔댕댜더덕덖던덛덜덞덟덤덥덧덩덫덮데덱덴델뎀뎁뎃뎄뎅뎌뎐뎔뎠뎡뎨뎬도독돈돋돌돎돐돔돕돗동돛돝돠돤돨돼됐되된될됨됩됫됬됴두둑둔둘둠둡둣둥둬뒀뒈뒝뒤뒨뒬뒵뒷뒹듀듄듈듐듕드득든듣들듦듬듭듯등듸디딕딘딛딜딤딥딧딨딩딪따딱딴딸땀땁땃땄땅땋때땍땐땔땜땝땟땠땡떠떡떤떨떪떫떰떱떳떴떵떻떼떽뗀뗄뗌뗍뗏뗐뗑뗘뗬또똑똔똘똥똬똴뙈뙤뙨뚜뚝뚠뚤뚫뚬뚱뛔뛰뛴뛸뜀뜁뜅뜨뜩뜬뜯뜰뜸뜹뜻띄띈띌띔띕띠띤띨띰띱띳띵라락란랄람랍랏랐랑랒랖랗래랙랜랠램랩랫랬랭랴략랸럇량러럭런럴럼럽럿렀렁렇레렉렌렐렘렙렛렝려력련렬렴렵렷렸령례롄롑롓로록론롤롬롭롯롱롸롼뢍뢨뢰뢴뢸룀룁룃룅료룐룔룝룟룡루룩룬룰룸룹룻룽뤄뤘뤠뤼뤽륀륄륌륏륑류륙륜률륨륩륫륭르륵른를름릅릇릉릊릍릎리릭린릴림립릿링마막만많맏말맑맒맘맙맛망맞맡맣매맥맨맬맴맵맷맸맹맺먀먁먈먕머먹먼멀멂멈멉멋멍멎멓메멕멘멜멤멥멧멨멩며멱면멸몃몄명몇몌모목몫몬몰몲몸몹못몽뫄뫈뫘뫙뫼묀묄묍묏묑묘묜묠묩묫무묵묶문묻물묽묾뭄뭅뭇뭉뭍뭏뭐뭔뭘뭡뭣뭬뮈뮌뮐뮤뮨뮬뮴뮷므믄믈믐믓미믹민믿밀밂밈밉밋밌밍및밑바박밖밗반받발밝밞밟밤밥밧방밭배백밴밸뱀뱁뱃뱄뱅뱉뱌뱍뱐뱝버벅번벋벌벎범법벗벙벚베벡벤벧벨벰벱벳벴벵벼벽변별볍볏볐병볕볘볜보복볶본볼봄봅봇봉봐봔봤봬뵀뵈뵉뵌뵐뵘뵙뵤뵨부북분붇불붉붊붐붑붓붕붙붚붜붤붰붸뷔뷕뷘뷜뷩뷰뷴뷸븀븃븅브븍븐블븜븝븟비빅빈빌빎빔빕빗빙빚빛빠빡빤빨빪빰빱빳빴빵빻빼빽뺀뺄뺌뺍뺏뺐뺑뺘뺙뺨뻐뻑뻔뻗뻘뻠뻣뻤뻥뻬뼁뼈뼉뼘뼙뼛뼜뼝뽀뽁뽄뽈뽐뽑뽕뾔뾰뿅뿌뿍뿐뿔뿜뿟뿡쀼쁑쁘쁜쁠쁨쁩삐삑삔삘삠삡삣삥사삭삯산삳살삵삶삼삽삿샀상샅새색샌샐샘샙샛샜생샤샥샨샬샴샵샷샹섀섄섈섐섕서석섞섟선섣설섦섧섬섭섯섰성섶세섹센셀셈셉셋셌셍셔셕션셜셤셥셧셨셩셰셴셸솅소속솎손솔솖솜솝솟송솥솨솩솬솰솽쇄쇈쇌쇔쇗쇘쇠쇤쇨쇰쇱쇳쇼쇽숀숄숌숍숏숑수숙순숟술숨숩숫숭숯숱숲숴쉈쉐쉑쉔쉘쉠쉥쉬쉭쉰쉴쉼쉽쉿슁슈슉슐슘슛슝스슥슨슬슭슴습슷승시식신싣실싫심십싯싱싶싸싹싻싼쌀쌈쌉쌌쌍쌓쌔쌕쌘쌜쌤쌥쌨쌩썅써썩썬썰썲썸썹썼썽쎄쎈쎌쏀쏘쏙쏜쏟쏠쏢쏨쏩쏭쏴쏵쏸쐈쐐쐤쐬쐰쐴쐼쐽쑈쑤쑥쑨쑬쑴쑵쑹쒀쒔쒜쒸쒼쓩쓰쓱쓴쓸쓺쓿씀씁씌씐씔씜씨씩씬씰씸씹씻씽아악안앉않알앍앎앓암압앗았앙앝앞애액앤앨앰앱앳앴앵야약얀얄얇얌얍얏양얕얗얘얜얠얩어억언얹얻얼얽얾엄업없엇었엉엊엌엎에엑엔엘엠엡엣엥여역엮연열엶엷염엽엾엿였영옅옆옇예옌옐옘옙옛옜오옥온올옭옮옰옳옴옵옷옹옻와왁완왈왐왑왓왔왕왜왝왠왬왯왱외왹왼욀욈욉욋욍요욕욘욜욤욥욧용우욱운울욹욺움웁웃웅워웍원월웜웝웠웡웨웩웬웰웸웹웽위윅윈윌윔윕윗윙유육윤율윰윱윳융윷으윽은을읊음읍읏응읒읓읔읕읖읗의읜읠읨읫이익인일읽읾잃임입잇있잉잊잎자작잔잖잗잘잚잠잡잣잤장잦재잭잰잴잼잽잿쟀쟁쟈쟉쟌쟎쟐쟘쟝쟤쟨쟬저적전절젊점접젓정젖제젝젠젤젬젭젯젱져젼졀졈졉졌졍졔조족존졸졺좀좁좃종좆좇좋좌좍좔좝좟좡좨좼좽죄죈죌죔죕죗죙죠죡죤죵주죽준줄줅줆줌줍줏중줘줬줴쥐쥑쥔쥘쥠쥡쥣쥬쥰쥴쥼즈즉즌즐즘즙즛증지직진짇질짊짐집짓징짖짙짚짜짝짠짢짤짧짬짭짯짰짱째짹짼쨀쨈쨉쨋쨌쨍쨔쨘쨩쩌쩍쩐쩔쩜쩝쩟쩠쩡쩨쩽쪄쪘쪼쪽쫀쫄쫌쫍쫏쫑쫓쫘쫙쫠쫬쫴쬈쬐쬔쬘쬠쬡쭁쭈쭉쭌쭐쭘쭙쭝쭤쭸쭹쮜쮸쯔쯤쯧쯩찌찍찐찔찜찝찡찢찧차착찬찮찰참찹찻찼창찾채책챈챌챔챕챗챘챙챠챤챦챨챰챵처척천철첨첩첫첬청체첵첸첼쳄쳅쳇쳉쳐쳔쳤쳬쳰촁초촉촌촐촘촙촛총촤촨촬촹최쵠쵤쵬쵭쵯쵱쵸춈추축춘출춤춥춧충춰췄췌췐취췬췰췸췹췻췽츄츈츌츔츙츠측츤츨츰츱츳층치칙친칟칠칡침칩칫칭카칵칸칼캄캅캇캉캐캑캔캘캠캡캣캤캥캬캭컁커컥컨컫컬컴컵컷컸컹케켁켄켈켐켑켓켕켜켠켤켬켭켯켰켱켸코콕콘콜콤콥콧콩콰콱콴콸쾀쾅쾌쾡쾨쾰쿄쿠쿡쿤쿨쿰쿱쿳쿵쿼퀀퀄퀑퀘퀭퀴퀵퀸퀼큄큅큇큉큐큔큘큠크큭큰클큼큽킁키킥킨킬킴킵킷킹타탁탄탈탉탐탑탓탔탕태택탠탤탬탭탯탰탱탸턍터턱턴털턺텀텁텃텄텅테텍텐텔템텝텟텡텨텬텼톄톈토톡톤톨톰톱톳통톺톼퇀퇘퇴퇸툇툉툐투툭툰툴툼툽툿퉁퉈퉜퉤튀튁튄튈튐튑튕튜튠튤튬튱트특튼튿틀틂틈틉틋틔틘틜틤틥티틱틴틸팀팁팃팅파팍팎판팔팖팜팝팟팠팡팥패팩팬팰팸팹팻팼팽퍄퍅퍼퍽펀펄펌펍펏펐펑페펙펜펠펨펩펫펭펴편펼폄폅폈평폐폘폡폣포폭폰폴폼폽폿퐁퐈퐝푀푄표푠푤푭푯푸푹푼푿풀풂품풉풋풍풔풩퓌퓐퓔퓜퓟퓨퓬퓰퓸퓻퓽프픈플픔픕픗피픽핀필핌핍핏핑하학한할핥함합핫항핳해핵핸핼햄햅햇했행햐향허헉헌헐헒험헙헛헝헤헥헨헬헴헵헷헹혀혁현혈혐협혓혔형혜혠혤혭호혹혼홀홅홈홉홋홍홑화확환활홧황홰홱홴횃횅회획횐횔횝횟횡효횬횰횹횻후훅훈훌훑훔훗훙훠훤훨훰훵훼훽휀휄휑휘휙휜휠휨휩휫휭휴휵휸휼흄흇흉흐흑흔흖흗흘흙흠흡흣흥흩희흰흴흼흽힁히힉힌힐힘힙힛힝힣ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎㄲㄸㅃㅆㅉㄺㅀㄻㄼㅄㄳㄶㄵㄽㅏㅑㅓㅕㅗㅛㅜㅠㅡㅣㅒㅖ"""
RAW_DATA = "·—、。《》一丁七万丈三上下不与丐丑专且世丘业丛东丝丢两严丧个中丰串临丹为主丽举乃久么义之乌乎乏乐乔乘乞也习乡书买乱乳了予争事二于亏云互五井亚些亡交产亨享亭亮亲亵人什仁仅仆仇今介仍从仓仔他付代令以仪们仰件价任份仿企伊伍伏伐休众优伙会伟传伤伦伪伯估伴伸似但位低住佐佑体何余佛作佝你佣佩佬佳使例侍侏供依侠侣侥侦侧侯侵便促俄俊俗俘保信俩修俯俱倍倒候倚借债值倾假偎偏做停健偶偷偻偿傅傍储催傲傻像僚僧僵僻儒儿兀允元兄充兆先光克免兑兔兜入全八公六兮兰共关兴兵其具典养兼兽内册再冒冗写军农冠冢冥冬冰冲决况冷冻净凄准凉凌减凑凛凝几凡凭凯凳凶凹出击函刀刃分切划列则刚创初删判利别刮到制刷刺刻剂剃削前剑剔剖剥剧剩剪副割剽劈力劝办功加务劣动助努劫励劲劳势勃勇勉勋勒募勤勺勾勿包匆匕化北匙匠匪匹区医匿十千升半华协卑卒卓单卖南博卜占卡卢卧卫印危即却卷卸厂厄厅历厉压厌厚原厨厩厮去参又叉及友双反发取受变叙叛叠口古句另叨只叫召叮可台史右叶号司叹吃各合吉吊同名后吐向吓吗君吝吞吟吠否吧吨含听启吱吵吸吹吻吼呆呈告呕员呜呢周呱味呻呼命咆和咏咒咔咕咙咩咫咬咯咳咸咽哀品哇哈响哎哑哒哗哝哟哥哦哨哪哭哮哼哽唇唉唐唠唤唧售唯唱唾啃商啊啜啤啦啪啬啮啸喀喂喃善喉喊喋喔喘喜喝喧喷喻嗅嗒嗓嗜嗝嗡嗤嗨嗫嗯嗷嗽嘀嘈嘉嘎嘟嘲嘴嘶嘿噜噢噤器噩噪噬噼嚎嚏嚓嚣嚷嚼囊囔囚四回因团囤园困囱围固国图圆圈土圣在地场圾址坊坍坏坐坑块坚坛坟坠坡坦坳垂垃型垒垢垩垫垮埃埋城域培基堂堆堕堞堡堤堪堵塌塑塔塘塞填境墅墓墙增墟墨壁壑壤士壮声壳壶处备复夏外多夜够大天太夫央失头夸夹夺奄奇奈奉奋奏契奔奖套奢奥女奴奶她好如妄妆妇妈妒妓妖妙妥妪妹妻姆始姐姑姓委姬姿威娅娇娘娜娱婆婉婊婪婴媒嫉嫩嬉子孔孕字存孢季孤学孩孵孽宁它宅宇守安完宏宗官宙定宜宝实宠审客宣室宫宰害宴家容宽宾寂寄密寇富寒寝察寡寥寨寸对寻导寿封射将尊小少尔尖尘尚尝尤尬就尴尸尺尼尽尾尿局屁层居屈屋屎屏屑展属屠履山屹屿岁岌岔岖岗岛岩岬岸峋峙峡峦峭峰峻崇崎崖崛崩崽嵌嶙巅州巡巢工左巧巨巫差己已巴巷巾币市布帆师希帐帕帘帜帝带席帮帷常帽幅幕干平年并幸幻幼幽广庄庆庇床序库应底店府庞废度座庭康廉廊廓延廷建开异弃弄弋式弓引弗弛弟张弥弦弧弩弯弱弹强归当录彗形彩彭彰影役彻彼往征径待很徊律徒得徘御循微德徽心必忆忌忍志忘忙忠忧快念忽怀态怎怏怒怕怖怜思急性怨怪怯总恋恍恐恒恕恙恢恨恩恭息恰恳恶悄悉悔悚悟悠患悦您悬悯悲悴悼情惊惋惑惕惚惜惠惧惨惩惫惭惮惯惰想惹愁愈愉意愕愚感愣愤愧愿慈慌慎慑慢慧慨慰慵慷憋憎憔憩憾懂懈懒戈戏成我戒或战戛截戮戳戴户房所扁扇手才扎扑打扔托扣执扩扫扬扭扮扯扰扳扶批扼找承技抄把抑抓抔投抖抗折抚抛抠抢护报披抬抱抵抹押抽拂担拆拉拌拍拐拒拔拖拙招拜拟拢拥拦择括拭拯拱拳拼拾拿持挂指按挑挖挚挛挠挡挣挤挥挨挪振挺挽捂捅捆捉捏捕捞损捡换捧据捷掀授掉掌掏掐排掘掠探接控推掩措掳掷揉描提插握揭援揽搁搂搅搏搐搓搜搞搬搭携摆摇摊摔摘摧摩摸撂撑撒撕撞撤撬播撼擅操擎擦攀攒攥攫支收改攻放政故效敌敏救教敞敢散敬数敲整文斐斑斓斗料斜斟斥斧斩断斯新方施旁旅旋族旗无既日旦旧旨早旱时旷昂昆明昏易昔星映春昨是昵显晃晒晓晕晖晚晦晨普景晰晶智暂暖暗暴曲曳更曼曾替最月有朋服朗望朝期朦木未末本札术朱朴朵机朽杀杂权杆李杏材村杖杜束条来杯杰松板极构析林枚果枝枢枪枯架枷柄柏某染柔柜柠查柱柴栅标栈栋栏树栖栗样核根格桂桃桅框案桌档桥桶梁梅梦梧梨梭梯械梳检棉棋棍棒棕棘棚森棵棺椁椅植楚楼概榉榔榴槌槛槽模横橡橱檬欠次欢欣欲欺款歇歉歌止正此步武歪死殆殊残殓殖殴段殿毁毅母每毒比毕毙毛毫毯民气氛氧水永汀汁求汇汉汐汗汞池污汤汲汹汽沃沉沐沙沟没沥沦沫沮河沸油治沼沾沿泄泉泊法泛泞泡波泣泥注泪泰泳泵泻泽洁洋洒洗洛洞津洪活洼派流浅浆浇浊测浏浑浓浪浮浴海浸涂涅消涉涌涎涕涛涡润涨涩涯液涸淆淌淑淘淡淫深混淹添清渊渍渎渐渔渗渣温港渲渴游湖湛湾湿溃溅源溜溢溪溯溺滋滑滓滔滚滞满滤滥滩滴漂漆漉漏演漠漩漫潜潦潮澈澡激瀑灌火灭灯灰灵灶灼灾灿炉炎炖炬炭炸点炼炽烁烂烈烙烛烟烤烦烧烬热烹焉焕焚焦焰然煌煎煤照煮熄熊熏熔熟熬燃燕燥爆爪爬爱爵父爸爹爽片版牌牙牛牡牢牧物牲牵特牺犀犄犬犯状犷犹狂狈狗狠狡狩独狭狮狱狼猎猛猜猥猪猫献猾獠玄率玉王玛玩玫环现玷玺玻珀珊珍珠班球理琉琐琢琳琴瑕瑚瑞瑟瑰璃瓜瓣瓦瓶甘甚甜生用甩田由甲电男甸画畅界畏畔留畜略番畴畸疆疏疑疗疚疤疫疮疯疲疸疼疾痂病症痉痊痍痒痕痘痛痪痴痹瘟瘠瘦瘪瘫瘴瘸癌癫登白百的皇皮皱盆盈益盏盐监盒盔盖盗盘盛盟目盯盲直相盾省眉看真眠眨眩眯眶眷眺眼着睁睐睛睡督睨睹睿瞎瞒瞥瞧瞪瞬瞭瞰瞳矗矛矢知矫短矮石矿码砌砍研砖砗砰破砸砺础硕硫硬确碌碍碎碑碗碧碰碱碾磅磨磲磺礁示礼社祇祈祖祝神祟祥祭祷祸禁福离秀私秃秋种科秒秘租秩积称移秽稀程稍税稚稠稳稻稼稿穴究穷穹空穿突窃窄窍窒窖窗窜窝窟窣窥窸窿立竖站竟章童竭端笑笔笛笞符笨第笼等筋筑筒答策筹签简箔算管箭箱篇篓篝篡篮篱篷簇簿籍米类粉粒粗粘粥粪粹精糊糕糖糙糟系紊素索紧紫累繁纠红约级纪纯纱纳纵纷纸纹纺线练组绅细织终绊绍经绑绒结绕绘给络绝绞统继绩绪续绯绳维绵绷绸综绽绿缅缎缓缕编缘缚缝缠缩缺罐网罕罗罚罩罪置署羊美羔羞群羽翁翅翠翡翩翻翼耀老考者而耍耐耗耳耸耻耽聊聋职联聚聪肃肆肉肋肌肖肚肝肠股肢肤肥肩肪肮肯育肴肺肾肿胀胁胃胄胆背胖胜胞胡胧胳胶胸能脂脆脉脊脏脑脓脖脚脱脸脾腌腐腔腕腥腮腰腹腺腻腾腿膀膊膏膛膜膝膨臂臃臣自臭至致舌舍舒舔舞舟航般舱舵舷船艇艘良艰色艳艺艾节芒芙芜芝芬芭花芳芽苍苏苗苟若苦英苹茂范茧茨茫草荐荒荡荣药莉莎莫莱莲获菇菌菜菲萄萎萝萤营萨落葆著葡葬葱蒂蒙蒜蒸蓄蓝蓬蔑蔓蔚蔬蔼蔽蕨蕴蕾薄薪薯薰藉藏藤藻蘑蘸虎虏虐虑虔虚虫虽蚀蚂蚊蚋蚕蚣蛇蛋蛎蛙蛛蛮蛰蛹蛾蜂蜇蜈蜍蜒蜗蜘蜜蜡蜥蜴蜷蜿蝇蝎蝗蝙蝠蝾螃螈融螺蟒蟥蟹蟾蠕蠢血衅行衍衔街衡衣补表衫衬衰衷袋袍袖袤被袭裁裂装裔裕裙裡裤裨裴裸裹褐褓褛褪褴襁西要覆见观规觅视览觉觊觎觑角解触言誉誓警计订认讧讨让训议记讲讶讷许论讽设访诀证评诅识诉词译试诗诚话诞诡询该详语误诱说诵请诸诺读谁调谅谈谊谋谎谐谒谓谕谚谜谢谣谦谨谬谱谷豁豆象豪豫貌贝负财责贤败账货质贩贪贫贬购贯贱贴贵贷贸费贼贾贿赂赃资赋赌赎赏赐赔赖赚赛赞赠赢赤赫走赴赶起趁超越趟趣足跃跄跌跎跑跚距跟跤跨跪路跳践踉踏踝踢踩踪踱踵踹蹈蹉蹒蹬蹲躁身躬躯躲躺车轨转轮软轰轴轻载较辆辈辉辐辑输辘辙辛辜辞辣辨辩辫辰辱边达迄迅过迈迎运近返还这进远违连迟迪迫述迷迹追退送适逃逆选逊透逐递途逗通逛逝逞速造逢逸逻逼逾遇遍遏道遗遣遥遭遮遵避邀邃那邦邪邮邸邻郁郊部都鄙酋配酒酗酝酥酪酬酱酷酸酿醉醒醺采释里重野量金鉴针钉钓钝钟钢钥钩钮钱钳钻铁铃铜铠铰铲银铺链销锁锄锅锈锋锐错锚锡锤锥键锯锻镀镇镐镖镜镰镳镶长门闩闪闭问闯闲间闷闹闻阁阅阉阔队阱防阳阴阵阶阻阿附际陆陈陋陌降限陛陡院除陨险陪陵陶陷隆随隐隔隘隙障隧隶隼难雀雄雅集雇雉雌雕雨雪零雷雾需霄震霉霍霜露霾青静非靠面革靴鞋鞘鞠鞭韧韭音韵響页顶项顺须顽顾顿颂预颅领颇颈颊颌颐频颗题颚颜额颠颤颧风飘飞食餐饥饪饭饮饰饱饵饶饼饿馅馆馈首香馨马驭驯驱驳驴驶驻驾骂验骑骗骚骡骨骰骷骸骼髅髓高髻鬃鬼魁魂魄魅魇魈魔鱼鲁鲍鲜鲨鲸鳄鳍鳗鳝鳞鸟鸡鸣鸥鸦鸭鸿鹰麦麻黄黎黏黑黔默黠鼎鼓鼠鼹鼻鼾齐齿龄龙龛龟！（），：；？�"
# <-- 把 C# 里的 rawData 整段粘过来（建议三引号 """..."""）

def get_chinese_texture() -> str:
    """
    - 返回 rawData 字符串
    """
    if not RAW_DATA or RAW_DATA == "PASTE_YOUR_RAWDATA_HERE":
        # 你没填 rawData 时给个最小可跑字符集，方便验证 UI 流程
        return "你好呀"
    return RAW_DATA

def build_character_chart(chars: str) -> dict:
    """
    - 按输入顺序 index++
    - 若字符重复，dict 会覆盖为最后一次出现的 index
    """
    chart = {}
    idx = 90
    for ch in chars:
        chart[str(ch)] = idx
        #print(str(ch), idx)
        idx += 1
    return chart


def json_printer(chars: str) -> None:
    """
    把 language pack JSON 打印到控制台
    """
    result = {
        "languageCode": "ko",
        "name": "Chinese",
        "description": "中文字体语言包",
        "version": "1.0.0",
        "fontFilesPath": "fonts",
        "translationFilesPath": "for_translations",
        "characterChart": build_character_chart(chars),
    }
    # ensure_ascii=False 保证控制台输出中文不变成 \uXXXX
    print(json.dumps(result, ensure_ascii=False, indent=2))


def safe_load_font(font_path: Optional[str], size: int):
    """
    尝试加载 TTF 字体；如果没选字体则退回 PIL 默认字体（注意：默认字体不支持中文）
    """
    if font_path and os.path.exists(font_path):
        return ImageFont.truetype(font_path, size=size)
    return ImageFont.load_default()


def generate_atlas_image(
    chars: str,
    atlas_width: int,
    cell_w: int,
    cell_h: int,
    font_path: Optional[str],
    font_size: int,
    offset_x: int,
    offset_y: int,
    text_color_rgba: Tuple[int, int, int, int],
    grid_top: bool,
    grid_bottom: bool,
    grid_left: bool,
    grid_right: bool,
) -> Tuple[Image.Image, int, int]:
    """
    - bottom-up 排列（左下开始→向右→满行上移）
    - 可选画紫色 grid
    - 用 1-bit mask 方式贴字
    """
    if cell_w <= 0 or cell_h <= 0:
        raise ValueError("单元格宽高必须 > 0")

    columns = atlas_width // cell_w
    if columns < 1:
        raise ValueError("单元格宽度大于图集宽度（Cell Width > Atlas Width）")

    total_rows = math.ceil(len(chars) / columns)
    atlas_height = total_rows * cell_h

    # 透明背景
    atlas = Image.new("RGBA", (atlas_width, atlas_height), (0, 0, 0, 0))
    draw = ImageDraw.Draw(atlas)

    font = safe_load_font(font_path, font_size)

    # 类似 C# drawFont.GetHeight(g)
    try:
        ascent, descent = font.getmetrics()
        font_height = ascent + descent
    except Exception:
        bbox = font.getbbox("Hg")
        font_height = bbox[3] - bbox[1]

    grid_color = (255, 0, 255, 255)  # Magenta

    # Always draw inner grid lines first.
    grid_width = columns * cell_w
    for c in range(1, columns):
        x = c * cell_w
        draw.line([(x, 0), (x, atlas_height - 1)], fill=grid_color, width=1)
    for r in range(1, total_rows):
        y = r * cell_h
        draw.line([(0, y), (grid_width - 1, y)], fill=grid_color, width=1)

    # Then optionally draw outer borders by direction.
    if grid_top:
        draw.line([(0, 0), (grid_width - 1, 0)], fill=grid_color, width=1)
    if grid_bottom:
        draw.line([(0, atlas_height - 1), (grid_width - 1, atlas_height - 1)], fill=grid_color, width=1)
    if grid_left:
        draw.line([(0, 0), (0, atlas_height - 1)], fill=grid_color, width=1)
    if grid_right:
        draw.line([(grid_width - 1, 0), (grid_width - 1, atlas_height - 1)], fill=grid_color, width=1)

    for i, ch in enumerate(chars):
        col = i % columns
        row_from_bottom = i // columns
        visual_row = (total_rows - 1) - row_from_bottom

        x_pos = col * cell_w
        y_pos = visual_row * cell_h
        draw_x = x_pos + offset_x
        draw_y = y_pos + (cell_h - font_height) + offset_y

        # 用 1-bit mask 方式贴字（尽量接近 SingleBitPerPixel）
        try:
            mask_core = font.getmask(str(ch), mode="1")  # 无抗锯齿
            mask_img = Image.frombytes("L", mask_core.size, bytes(mask_core))
        except Exception:
            draw.text((draw_x, draw_y), str(ch), font=font, fill=text_color_rgba)
            continue

        colored = Image.new("RGBA", mask_img.size, text_color_rgba)
        atlas.paste(colored, (int(draw_x), int(draw_y)), mask_img)

    return atlas, total_rows, atlas_height


# ========= 2) UI =========

class AtlasApp(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("字体图集生成器（底部向上排布）")
        self.geometry("1100x850")

        # 状态
        self.font_path: Optional[str] = None
        self.text_color = (0, 0, 0, 255)  # RGBA
        self._preview_imgtk = None  # 防止 PhotoImage 被 GC

        # Split：左设置 / 右预览
        paned = ttk.Panedwindow(self, orient=tk.HORIZONTAL)
        paned.pack(fill=tk.BOTH, expand=True)

        self.left = ttk.Frame(paned, width=320)
        self.right = ttk.Frame(paned)
        paned.add(self.left, weight=0)
        paned.add(self.right, weight=1)

        self._build_left_panel()
        self._build_right_panel()

        # 默认值（对齐你 C# 的默认）
        self.var_atlas_width.set(153)
        self.var_cell_w.set(17)
        self.var_cell_h.set(17)
        self.var_font_size.set(10)
        self.var_offset_x.set(-2)
        self.var_offset_y.set(2)
        self.var_grid_top.set(True)
        self.var_grid_left.set(False)
        self.var_grid_right.set(True)
        self.var_grid_bottom.set(False)

    def _build_left_panel(self):
        # 左侧滚动设置区（Canvas + Frame）
        canvas = tk.Canvas(self.left, highlightthickness=0)
        vsb = ttk.Scrollbar(self.left, orient=tk.VERTICAL, command=canvas.yview)
        canvas.configure(yscrollcommand=vsb.set)

        vsb.pack(side=tk.RIGHT, fill=tk.Y)
        canvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)

        self.settings_frame = ttk.Frame(canvas)
        self.settings_frame_id = canvas.create_window((0, 0), window=self.settings_frame, anchor="nw")

        def _on_frame_config(_):
            canvas.configure(scrollregion=canvas.bbox("all"))

        def _on_canvas_config(e):
            canvas.itemconfigure(self.settings_frame_id, width=e.width)

        self.settings_frame.bind("<Configure>", _on_frame_config)
        canvas.bind("<Configure>", _on_canvas_config)

        # 变量
        self.var_atlas_width = tk.IntVar()
        self.var_cell_w = tk.IntVar()
        self.var_cell_h = tk.IntVar()
        self.var_font_size = tk.IntVar()
        self.var_offset_x = tk.IntVar()
        self.var_offset_y = tk.IntVar()
        self.var_grid_top = tk.BooleanVar()
        self.var_grid_bottom = tk.BooleanVar()
        self.var_grid_left = tk.BooleanVar()
        self.var_grid_right = tk.BooleanVar()

        row = 0

        def header(text):
            nonlocal row
            lab = ttk.Label(self.settings_frame, text=text, font=("Segoe UI", 10, "bold"))
            lab.grid(row=row, column=0, columnspan=3, sticky="w", padx=10, pady=(12, 6))
            row += 1

        def label(text):
            nonlocal row
            ttk.Label(self.settings_frame, text=text).grid(row=row, column=0, columnspan=3, sticky="w", padx=10)
            row += 1

        def spin(var, from_, to_):
            nonlocal row
            sp = ttk.Spinbox(self.settings_frame, from_=from_, to=to_, textvariable=var, width=12)
            sp.grid(row=row, column=0, sticky="w", padx=10, pady=(2, 8))
            row += 1
            return sp

        header("[ 1. 尺寸设置 ]")
        label("图集宽度（固定）：")
        spin(self.var_atlas_width, 72, 8192)
        label("单元格宽度（Cell Width）：")
        spin(self.var_cell_w, 8, 256)
        label("单元格高度（Cell Height）：")
        spin(self.var_cell_h, 8, 256)

        header("[ 2. 字体与颜色 ]")
        self.lbl_font = ttk.Label(self.settings_frame, text="字体：未选择")
        self.lbl_font.grid(row=row, column=0, columnspan=3, sticky="w", padx=10)
        row += 1

        btn_font = ttk.Button(self.settings_frame, text="选择字体（TTF/OTF）", command=self.on_pick_font)
        btn_font.grid(row=row, column=0, sticky="w", padx=10, pady=(4, 8))
        row += 1

        label("字体大小：")
        spin(self.var_font_size, 4, 200)

        label("文字颜色：")
        btn_color = ttk.Button(self.settings_frame, text="选择颜色", command=self.on_pick_color)
        btn_color.grid(row=row, column=0, sticky="w", padx=10, pady=(2, 8))

        self.color_preview = tk.Canvas(self.settings_frame, width=70, height=20, highlightthickness=1)
        self.color_preview.grid(row=row, column=1, sticky="w", padx=10, pady=(2, 8))
        self._refresh_color_preview()
        row += 1

        header("[ 3. 位置微调 ]")
        label("X 偏移（左右）：")
        spin(self.var_offset_x, -50, 50)
        label("Y 偏移（上下）：")
        spin(self.var_offset_y, -50, 50)

        header("[ 4. 网格线（紫色）方向 ]")
        frm1 = ttk.Frame(self.settings_frame)
        frm1.grid(row=row, column=0, columnspan=3, sticky="w", padx=10)
        ttk.Checkbutton(frm1, text="上边（Top）", variable=self.var_grid_top).pack(side=tk.LEFT)
        ttk.Checkbutton(frm1, text="下边（Bottom）", variable=self.var_grid_bottom).pack(side=tk.LEFT, padx=(10, 0))
        row += 1

        frm2 = ttk.Frame(self.settings_frame)
        frm2.grid(row=row, column=0, columnspan=3, sticky="w", padx=10, pady=(4, 8))
        ttk.Checkbutton(frm2, text="左边（Left）", variable=self.var_grid_left).pack(side=tk.LEFT)
        ttk.Checkbutton(frm2, text="右边（Right）", variable=self.var_grid_right).pack(side=tk.LEFT, padx=(10, 0))
        row += 1

        btn_preview = ttk.Button(self.settings_frame, text="生成预览（刷新）", command=self.on_preview)
        btn_preview.grid(row=row, column=0, sticky="we", padx=10, pady=(10, 6))
        row += 1

        btn_save = ttk.Button(self.settings_frame, text="保存 PNG 图集", command=self.on_save_png)
        btn_save.grid(row=row, column=0, sticky="we", padx=10, pady=(0, 10))
        row += 1

        self.lbl_info = ttk.Label(self.settings_frame, text="就绪。", foreground="blue")
        self.lbl_info.grid(row=row, column=0, columnspan=3, sticky="w", padx=10, pady=(0, 10))

    def _build_right_panel(self):
        # 右侧预览：可滚动 Canvas
        self.preview_canvas = tk.Canvas(self.right, bg="#555555", highlightthickness=0)
        xsb = ttk.Scrollbar(self.right, orient=tk.HORIZONTAL, command=self.preview_canvas.xview)
        ysb = ttk.Scrollbar(self.right, orient=tk.VERTICAL, command=self.preview_canvas.yview)
        self.preview_canvas.configure(xscrollcommand=xsb.set, yscrollcommand=ysb.set)

        self.preview_canvas.grid(row=0, column=0, sticky="nsew")
        ysb.grid(row=0, column=1, sticky="ns")
        xsb.grid(row=1, column=0, sticky="ew")

        self.right.rowconfigure(0, weight=1)
        self.right.columnconfigure(0, weight=1)

        self.preview_image_id = None

    def _refresh_color_preview(self):
        r, g, b, _ = self.text_color
        self.color_preview.delete("all")
        self.color_preview.create_rectangle(0, 0, 70, 20, fill=f"#{r:02x}{g:02x}{b:02x}", outline="")

    def on_pick_color(self):
        rgb, hex_ = colorchooser.askcolor(title="选择文字颜色")
        if not hex_:
            return
        r = int(hex_[1:3], 16)
        g = int(hex_[3:5], 16)
        b = int(hex_[5:7], 16)
        self.text_color = (r, g, b, 255)
        self._refresh_color_preview()

    def on_pick_font(self):
        path = filedialog.askopenfilename(
            title="选择字体文件（TTF/OTF）",
            filetypes=[("字体文件", "*.ttf *.otf"), ("所有文件", "*.*")]
        )
        if not path:
            return
        self.font_path = path
        self.lbl_font.config(text=f"字体：{os.path.basename(path)}")

    def _do_generate(self, save_mode: bool):
        chars = get_chinese_texture()

        atlas_width = int(self.var_atlas_width.get())
        cell_w = int(self.var_cell_w.get())
        cell_h = int(self.var_cell_h.get())
        font_size = int(self.var_font_size.get())
        offset_x = int(self.var_offset_x.get())
        offset_y = int(self.var_offset_y.get())

        img, rows, atlas_h = generate_atlas_image(
            chars=chars,
            atlas_width=atlas_width,
            cell_w=cell_w,
            cell_h=cell_h,
            font_path=self.font_path,
            font_size=font_size,
            offset_x=offset_x,
            offset_y=offset_y,
            text_color_rgba=self.text_color,
            grid_top=self.var_grid_top.get(),
            grid_bottom=self.var_grid_bottom.get(),
            grid_left=self.var_grid_left.get(),
            grid_right=self.var_grid_right.get(),
        )

        self.lbl_info.config(text=f"字符数：{len(chars)} | 尺寸：{atlas_width}x{atlas_h} | 行数：{rows}")

        # 打印 language_pack.json（可按你需要关闭）
        json_printer(chars)

        if save_mode:
            out_path = filedialog.asksaveasfilename(
                title="保存 PNG 图集",
                defaultextension=".png",
                filetypes=[("PNG 图片", "*.png")],
                initialfile="FontAtlas_BottomUp.png"
            )
            if not out_path:
                return
            img.save(out_path, "PNG")
            messagebox.showinfo("完成", "保存成功！")
        else:
            self._show_preview(img)

    def _show_preview(self, img: Image.Image):
        imgtk = ImageTk.PhotoImage(img)
        self._preview_imgtk = imgtk  # keep ref

        self.preview_canvas.delete("all")
        self.preview_image_id = self.preview_canvas.create_image(0, 0, anchor="nw", image=imgtk)
        self.preview_canvas.configure(scrollregion=(0, 0, img.width, img.height))

        self.after(50, self._center_if_small)

    def _center_if_small(self):
        if not self._preview_imgtk:
            return
        img_w = self._preview_imgtk.width()
        img_h = self._preview_imgtk.height()
        view_w = self.preview_canvas.winfo_width()
        view_h = self.preview_canvas.winfo_height()
        if img_w < view_w and img_h < view_h and self.preview_image_id is not None:
            x = (view_w - img_w) // 2
            y = (view_h - img_h) // 2
            self.preview_canvas.coords(self.preview_image_id, x, y)
        elif self.preview_image_id is not None:
            self.preview_canvas.coords(self.preview_image_id, 0, 0)

    def on_preview(self):
        try:
            self._do_generate(save_mode=False)
        except Exception as e:
            messagebox.showerror("错误", f"生成失败：{e}")

    def on_save_png(self):
        try:
            self._do_generate(save_mode=True)
        except Exception as e:
            messagebox.showerror("错误", f"保存失败：{e}")


if __name__ == "__main__":
    # Windows 下控制台建议 UTF-8（新版 Windows Terminal 通常没问题）
    try:
        sys.stdout.reconfigure(encoding="utf-8")
    except Exception:
        pass

    app = AtlasApp()
    app.mainloop()

    """
    图集宽度
    huge    9*21 = 189
    large   9*21 = 189
    big     9*17 = 153
    tiny    9*8  = 72
    """

