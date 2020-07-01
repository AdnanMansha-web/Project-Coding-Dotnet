CREATE DATABASE  IF NOT EXISTS `project` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `project`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: project
-- ------------------------------------------------------
-- Server version	5.6.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `login` (
  `username` varchar(45) NOT NULL,
  `pasword` varchar(45) DEFAULT NULL,
  `image` varbinary(10000) DEFAULT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES ('Administrator','7360','ÿØÿà\0JFIF\0\0\0\0\0\0\0ÿÛ\0C\0	\Z\Z( \Z%!1\"%)+... 383,7(-.,ÿÛ\0C\n\n\n\r\Z,$ $,,,-,,,,,,,/,,-,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,ÿÀ\0\0Ì\0Ì\0ÿÄ\0\0\0\0\0\0\0\0\0\0\0\0\0ÿÄ\0E\0	\0\0\0\0!1AQa\"q‘¡2BRr±Á#3‚Ñ$Cb’¢²ðá%4csƒÂÒñÿÄ\0\0\0\0\0\0\0\0\0\0\0\0\0ÿÄ\05\0\0\0\0\0\0!12AQq\"a‘±B¡Áð3ÑCRá#4ÿÚ\0\0\0?\0î(\0€ UŽi\r= ¼Ò\0mpÁ›ÏcGÍGe±‡‰›Â¹O¢)užg”ÚšÆÝÏë;·Td<J§-dŸ…£¥KÄÍ]M}dŸ‰W åØFj	_cêÉ•P^FºjgZ¢¡Ç‰•ÅFç\'æþ¤Š+ÑŒ³36UNßûù]eY%æþ¦\"ü‘.M« 9Ê%Ð|ÅŠ–:›žHå§ƒò-ø¤è$êÔ´Àïx]ñžû]½þ*Õz¸¿ié¤¼<—j*Øæh|OkÚw´‚<•¥$ÖQ]¦¸fu“\0@\0@\0@\0@Ntë &\nkv>M¡œ›Åß%NýFß†=KTÑŸŠ]\n%,®2I¬KËžs<óÌ®{—9eÔ¼‘`ƒ-GšÆã;O’Ñ»uŠÆF\rMSÈ69jê&@k¥“`²‘†É”uöæ¶56ô®}ÄÔïÕxõ£öÞhXàò„ ¦°Î»£xÛ+ l±å¹Í;Xáµ§üØºµØ§£›88<3h·4\0€ \0€ \0€ »§8Ó©©þïñ¥=vÚ	ÚáØ<ì Ô[²u&¢½òç¡Ì Rg!égÚ@ÏTöüÎk‘Ë:fhñ:¹ó‚7Þµÿ\0©Ö0†O¯£Ä¶Û¸\'2E–¦¾,ÞÇðòÝ‰ð™Éý£lƒVxÿ\03\rˆî?ªÎÑ“SV.ë1Áàì;xÜVR0Í•ý¥§¶Ë9F¸dù¦lBÃjÆL¤jgœ“r†ÆóÑÖ2`­ldýÜýG±ßø÷òVt³Û<z•µÝútÎx@\0@\0@\0@£ÒÕs…\\Mi¶¤$‹mG8;É\\ícÌÒùô‹ál‡¡z2Ù\Z\'˜\\_¨Ã°ÛÚwöO©bRÁ}l+m¤[‰chÜb|KGu#U‰`ñKø‘´ž6³¼Fj>WBDòj]¢ôÀ[£ñs‰ñºÕÙ#t‘ªªÂd¦»é^ëÌNë7ÙmsÄƒ‡š>MRÊªs(\Z²3Ö>ÑlÂ—£4+rH·0FeYcÚñµŽ ƒôYO&­gƒô­<šÍk¸€|E×i–d@\0@\0@\0@ŸÒÝ!û\\.Üøõ{Øó\'µsu¼I?‘ÐÑò±ó.˜u(dlh´y(¡NYdÖÄ¤Q#Ü8…\"<±¨¥E\"¬PÉE¥j¯$MA™Š6JŽs\rWBê†îpsmÏ\\}5•åÊE~šÉ$ºØÁÈÁúS\0¿Ù`¾ÞŠ+ÿ\0 ]ˆxQÊŸ‰“ÖÆ¡\0@\0@\0@Xô…	©µý¨	vÖ÷*ºÈn©¿NK:Ií±/^	ô.c\\68â.«ÁeO‡‚kX¦H…°ö#FS\"JÕ‘,Y`«IÅ&\n¼‰â@œ(Y29v=­D ûÄ÷Ç‘W+yŠ ŸS^·5\raq\rI\0vœ‚È?NÑE©îµ£À\0»„rŸS2É€€ \0€ \0€ \0€ÇS{Ã±Í-=„XüÖ$²°ÌÅáåÍýw­tníc‹~‹™G‡Ë¡wP¾6×Ÿ?S~Ò­\"«>8¬3(‰1PÈ–$	Ê­2x\'*´Éâ@BÉ‘¡Å°¨¦7{sÁ Ûê‘²Qèo±K© Äp˜Ç8v‚2ã’’7ÉÉ#¦)6DÐšžºž;\\k‡;á`/?ÛæºÇ3H£l±ÏÑ+ªsB\0€ \0€ \0€ \0€ *ôC¢ª©‹s‹focÆ«­ù˜¹³[-’õä½â®/ò6í•n¤Bâ*Ã‘•4¯QJD‘D)\\ “&Š ÌåZLž(;”L•\ZùÊÕ’¢½¤ÓjÂF÷<î|‡š’…™˜¹â›ÐÎ	fÉVáë}Ü7y¤ùWsKŽ6¦„éêáT \0€ \0€ \0€ ¹¤qêOÛŽ´/üÝf_ó?2¡¬Ž\ZŸä\\Ó<ÆPüÌ‚e[y¾Ðé‘Ìm0I\"ÈÝD‹,ŠH–(‡+Ô-’¤A™ËFJ‘g-Y*F‘Ø[ëêã¦Žú­ëJíÌiÚO;ZÃ‰\nþŠ—/Ì§«µDí˜u ‰‘F5XÆ†´rUÝŒTVÆm·–HY0\0@\0@\0@¯ÇèLÐ=õí¬ÎOoY¾`(¯†úÜIižÉ¦WijõÚ×q8ãÅq7\'!™cy¦\'Êµr7Q#É\"³tˆ’È´d‰æzÕ’$y¦¢2KWÕ2kí\0I·`*Jésš‡©¬íPƒ—¡Ð°,*FÄ3qÖ{În{¸¸ü†À½%UF¸í‰Á²ÉY-ÌÙ)Â\0€ <K+Z.â\Z8’\0ñ(\r<ú_BÃgUC~OþÛ 3Ðé$ÆÑTB÷{¢Fë)7@m\0@\0@LÅiL8{öÀŸ]¾=oÍÉq5µ8Y•Ñ]4÷ÃhŠfT²XÚctË3ƒåXÉ²FK†ea&Þ¿©’@È[¯!¹Ü9ðyV#Z‡2êFäåÂ!hS¥Ä¡{¸ÉaÀtRX)´¯7Åûý™©b†½¾èëºqB\0€ (ÚU§’\Zj&‰&>CœqÛoÄáà9ì@S1(šî½}C¥vÛ9Ä7ò±¿DHê)ŽQRºNb+ù >>\nS”´òÁ}Ž-{\0;³Ø;ÅL›s:Y=Èn¹Íð“²çm¼¸[xI\rÁl ì²Ò\0€ \0€ &\'@Ù™ªìŽÖí;ŠŠê£l6²J¬uËr(µ°º\'–;Ö3¿¶©W7v«œgÈÄ\"yÜ{òXUÉùnŠ>ºœ79\Z;@ó*EOü™þˆ‡SŽÅ´C\\ñÌ¥oº1â&6·Ô¯ÕÔ>Wk<ß†à;¶ÉHÚPV7Ô¨‘ºÏ¿U›ìAÜ:¤ŸÓ¦¸Ñòk“™¨¹Û-‘è_08¥œ¿¡yödêÿ\0W«æ¬ÃWT¸Î=Ê²¦H±±áÂà‚8ƒp¬äˆªiš2\'àY/bs,†^±ì\\ÍOhÆe|¿ÓþË´èÜ–éðTXn%UÖ’CNâu<ÜüUeV²îg-«éö\'viêâ+,÷£÷Å@äÓú­–ŠKýÞŸ3G«‹üY42¢	:f6\n—\rÏ`$öìî ©]zÊ¹„·/Ÿó÷5ß¦ŸŠ;ŸÏ#‚é\\eÝñýš]–#U„öû=þ*Zuñ“ÙbÛ\";4-ÐyEƒdn‰â`z®Ö¾cVÙù.PãøÞPL×úÕ\'ƒOUÑ½ÊçaÔåûuH$7È ,ˆ\0€ \0€ \r7¥”ô×Úî÷[Ÿ‰UmÕÂudÐ¦RåðTcÇÅsŸ f¦­›¶÷õ¾c¹ruw“ÝŒp_¡mŽ2cž+ï?Ìáò*²tÈÐ°æEûKÕ2n¤Ì§`äÙaxWïdÉ£0/hò]&›ýÉþ_ä£ªÔ~þe;¯ûTåÿ\0»nL¸÷œüZ‹{É|ˆ«†Ôyº®JmpÙ¦m™äý]F¸€oºÊ9[5ðVùdµÕñËÈèÚ=€ÇHÐç€ú‚3;C/¹¿ª›uz%Ž¶~ˆ‚Û%wN#÷7N¥R²û,y“É\ZŠF\nª¶DÒé\ZÑ¼ý8•Ñ‹“Â5°iu>µµœÛìsšCTµj§[ødI-$ñÐË¥¤–j—²;²B@#,µw¸Ýu¡:õ±Û5‰/?çØ¯%KÊéèPˆUTÃö`ò)¯ë‘gÈÁ±­¾a‡n}œ•Ý%VU\r¶<ú\Zj,„å˜¬á£ûH4ž®B¢Q›#`öC¶9ÇõVˆ§CHØcdq‹1\rhä‡z:\0€ \0€ 1ÔNÖ4¹æÍä­e%–e&ÞÍ´³M\\òc‡ªÝ‡‰øÐ.Uú©Oˆð‹µÔ¡×–Pêç$seP‘²ó£4°G–e‚ýùýW[úhÙJO®\nŠç	¶‰µêzÖÙ˜Ís,Ò[áoÛ’ô/„—R\'B\\lÑrv[5\n„·mÇ$Û–3“iA€[­.ÝÍÿ\0Ûbééô8ø¬ú®Õçˆ}Jï¤\\cT\nXÏ]ö2[sw7¿åÚ¤Ö]…±S¼²Ÿv\0Ë-afk\ràÊY/\ZD’¥ââ1ªÏÛü\r»Öºf«Œõ2òéîÉuí¦>}}‹+g¹¹Ír]®MÉõf]xXD˜åRFDRNÒŠ¹]1èÃÑ9Œk$ÖÔ»˜÷$K.–ƒMìj}èbÉºªN=Y§­u\\¢ÓÏL÷YGƒœ.<Wf=Ÿ§_‡>å?ê,òdN×\0Á%T£ ´–ìöGr³ðW$¾„M¹3gˆ`5²Sºi­m·Ý4ÞB	µÞFËpòUnÖ.êS«œSRœÔeÁÐô±\n8º5‚ÖpÞ2q\'i\'n|Tº[»Ú”Ío¯»›‰¿V‚\0€ \0€ )Zs¤Q¶FQŒäp×w4z·æxpÂ£­±mÚYÓGâÉË%vg´ü×,²Fª9[‰–L3°èä ÆÖïkZ;¬½VG=õ(ÚoÚ±ì1Ç$q†´4íwy“ä­ì­2þ™I6›çƒÏö„÷]C]×kÔDØ˜èždeµâÛw]§/TŠ]nšµL§bRÂ}RÎ|°ýÍt¶ÛÞ(ÆO“ªãØ£)©Ý;öÕÜã“Z;þ«ÅÙ5îg£Œw<PÎéd|Ò½ä“ßÃ–îåÃœÜ¥–]ŠÂ&Š2³ÜÖ7‹Š×ø=FØ¯”Ís·4šÒÔÔ7«ÑÒ°Hõ0è­í½Î>.É¡Cª{{>?9“xüZ§òG¦J¸ŠE§e4ÃX_eÂ³DÒ²;ºeìƒÚðj4ß—]ÒÅ­«%µõssH¸î¸¿2»—«4š‡tbÿ\0æJõ8]Ww\'†Šô\Z9ƒU°Î÷¯šBÞvm®{û’ÎØ”£ˆ,?P´pƒÌå•è_0L*:hÚÈÚÑaÖp\0äª¶sæM²9c<,ñkA3NÃü…Õ½Îøy8¿Ð×¤¢þfŸÑŒ·‚Fû²eÞÑõº¿Ù2ÍM|ÍõëãOä\\—T \0@¿ÅK“?cFCÞqÉ£¼ÙGmŠ¸¹3hGsÁÀ¢®|•Ì–Cw=ÇXó ‹Y€¸›ÜÓl¾’‹I*)Ý¬î©ÚwsZi‘K	’6ï/hñ }VÕ¬É/™¬¸GYÃ¥-7× 9ç1ô&­l–FIàKEÇùÅ{ŽÅ†ýrý~ç[»Ÿ<–E8?L_Ráf°˜ÙzÀ¸÷4ùŠ¡Û÷÷iPŸ^_·—êM Óâ]ãüfœãŸj¨è£?³ÄHÄíŽyïÈrí^UvùatG¡ªY4­à©“I¨5^5â=öYèdÕÿ\0ù½{:ñ·{/–|³K¾:ÙšÖÙ£®hñé0ÐØÜï\'ý®PÎîÏ”WX¼ÿ\0?,›çf©7æF^q3¢Ñ‘¯[¦hâmè±‚Ñªá¬Î+³¤íyÕ“[—êQ»F¤ó³bZÙ5¡£Giw«l\"¢¿SHé¶òÞO1Êª)”xÅPe4Îÿ\0–@íwT5I,oŸ¤_ëÂ#PÌâ¾dFPÚžG{ÒeÜÐ>w]^ÉŽ*oÕšëÞf—È¸®¡D \0€ ]é%ñS4äHþdÜ0w\0ãÞ3´,åA{–ôñë#šLÀEÎVÎü>-§Á<’6Ì1€÷9Åä]­.$4n.y-›òDoQ\róûÆp[ÓýÈû¯¹¤ü,êTÎ°¹Ø3]ô²ðP9uOK#ä>Ûœîíò{ý5]ÕQ‡¢<v¦ÞòÙH°3û.Ãig|Á¶ÚÖkj¹Ý¹Xv¯þ¤¿n¡¥×	£²kÍ)²,’gd—FË¼p”2l*ªº?ÜMW‰_î É€õŸ½Ç—%»J+“~GLôqˆêë±ÛbAÝ|•mê›q/á“ê«sŠ’êm±jòõª~‡šåö–†Z[xð¾ö-i¯VÇæºÃ—?$íÚå²f­™\"ÝHÑÄ“ªXÈ†P4Zm\\lÊvæë‡<\r·ØÆù“n6]F¶VªüO—û/ç™¥1M¹ùy~ì¼hîözxã>°wÄsw™·rôÚj»ª£•}›æäl”äA\0@\0@pïIO\'–ûƒ\0ìÕ‰­ûY~U•·*Ñxy$k$ÙäÖ\rxöš<FDy-ßS÷„ÿ\0ÄÃñ·æ¤§û‘÷Dsð²ùŽÔêRÉm®\Zƒµý_‘+Õöm]æ¦+ÓŸ¡ÇÖÙÝÑ\'ò(råÜ½¶ååžF	ËÝL¥ä\\äÑªÑÀ\\».÷8÷¯”öž¯ú­Lí]ãÙp¤£¹¦0ô=ÝsË$¨]aa´í<5kªêúKÇ³=§oyýœAeõ1œð‰ØE\rìm•ìÑÄªWÚúh¯Íô:V‡t1çëœÜ~C¹sfòLå–X¨ëAf¤£Y›/´…Ð§´`¡ÜjVèýqüú¢¬éj[ëxf	ð;õ p{x˜ïZ]Ø»Öý,”—¦yþ{“C\\—¬2°ùFØÝáuÎ—gê£Ö¶YZŠŸI#$xt§kuGÑæ¤‡fêeËŽ«á\ZKSRóÏ·$züN:f’¯&ãìƒüO5<tq[Ý?_Â½½_ÌÓ³Ä°½<ß¹çCt}ï“íuiÖ§i\'Ú<†åÚìíO½ŸåþJš½BÇwÏüÅÛ9¡\0@\0@sÒÍe`~é#iïoT\r_Ç×ÇgÕtïáÁJT‰ÏP;\'7×o~N\"·ê	x9ý¢Œ)iþä}Ñ¤ü,´éTß„Ï‰ç¸Xy“à½ça×ÌìöG–í«qZ‡©O¯~Vã’—ýG­î4½Ú|ÏËÏüv&›¼»{é¿‘™ó£ØÙ™Éª£^ñÆz¾Û½îC’•%—Ô×¯“CIr\Z2oùÅU¶Ì,²jëÜðtÂvJáÉƒê¹Òm–§,-¨±[YÖ\nÍW\'ÚùÛs6 £Û–f+%V¯I?¨=âlÑZ®2‹Êo?\"Gãâ<·Lê\0ük÷ÿ\0ª¶­ÔôS—Ô‰×G¢ ÖélŽgßÅÇÌÙFè²ÏoÝ³=åPðþ…ëCtz)\"ŠªRe{Ú³kOfòsEÙõB*o—úûõs“q\\\"èº¥ € \0€ ¡é7ûE!{Eß\rÞ8–Û®pòªšÊ·×•ÕS<K§\\RéóZÄ{Güä¶¡†MÂ2¨‹þ£~jZ¿¹u÷4—…û2á]†tò€l¯kxñ%waþ§}š§Ck9Ýœg§ªÇêru=†µ»lV¤ñÓýÿ\0b‘ŠG«3ÛpíC«q²ûì¨öŸjälW$ÒÂI?ú-ht_ÒWÝ·—žY€f¹¥Ó\rLÄž‡ãwÐ)RQ[™¯^’65¡A9y²HÇÉÂ:W†´Yƒ7~—\\û&ç\"êJ¸—š‚ÐÖå•‡  ±íX\"‚ÜòÌôñê3YÛvžÍÁiŒ,±\'ºXE7Ljœ[—ñsútœ¹\'klx(.•Îõ‰\'šë¨¥Ð äßS#][£FcyYfõèðÿ\0»©¾ýî]?ö‘RÏ,Jc@€ \0€ \0B‹zAÑ3I!–&þÎó•¿vãìž‡‚ãjôýÛÜºj³rÃêSŠ¦LHÂŸi¢\'sÛ~â>–SÕÍ‘÷_r9ø_³6ROÕsóÌ¹Üó$ü—Õg¶Š}!²>xå+µ|>²Çêh\ZoÚWÊg\')9?3èIaaf”úŒõŽÓîÕm’ÜÇ^êƒE‡ÿ\0TR““Ë7Kó $‹½ÙÁRºÌðº)†Õ¹C\r¡m46ßµÇ‰Qx#ºDR“²XC„Èò÷l=ÁW®.Énd–ÉB;Qãª¹ÔÞÕ¥óçj6¢¾72Ÿ¤Uà{ “Þ·ÓÅ¾I¥„ŠLÛJì®‡1õ>kd·OƒSÉXÈÁúA¢ÕÃéG˜ïæ\ZÃæ»”,VŠvx™½Rš\0@\0@*švÈ×1ík…œÒ.<BÃI¬3)àå\Z]èîH‰’#ÚcÚö|>øóí\\»ôN?:z«½>$Psk¸{#Š‡IÅðOþKîîþÜ±èþÆÊ´Ú/Ê>‹é³\'­zcêðx.ÊŽílsêßÓ,ÑÉ%²nn;9s+æ‰.¬÷¦XbÕIÌž%i)nfé`Ÿ‡Sëc°y•Zë0°‰é¯/,éºƒj·¦xÌú£€â«Õ\rßègQoàDÊéÌ²3eì9ç±UºnÙì‰%PUÃtd‚ž Ýƒ™ÞJ±kTW„AZwY–T«+$ç¸o%sa6t¸EOysã´‘ó±§¯iJùåa\Z)ýb­®…VxY0LÁðÇÕLÈbsÎÝÍÜy\0¤®rÚŒJXYgèÊ\naQÆÝŒcX;\Z\0%ÝŒv¤Š-åäÎ²` \0€ \0€ Eh•5]ÌŒÕ’ßˆÞ«ûýîû­;¨9©µÊiý\r·KkŽN¤ºS>à\n\0Y¯ïÛ¼Û×vŒ5ZIÖÖ$ñíÔâé{6Z}R±<ÇŸ~…\Z§	–˜ÚxÜÇºÍ M;ì^\"ØÉ<5ƒÓÇ¡…FnXp E}—ñÍsµ\r²õ^ƒ«c–CÕÈÀx)5Rp§á)éâ¥oÄcÑê@Ö]lïnM’×CJPï_ŸØßWcrîÑUÒ¼hÜ¸lÏW¸•[ÿ\0¢Üùy¡æ¿Ÿ™F©Äõö—}|WFº#´îr\":qîçÄ›©°C“ßsr¶0ÍÞèMaaýãîÖ[‘ö»”õiçgDG+NÉ¢Z),Î¼®yH±<€ö[Éui¢5.:•g7#~¦4\0€ \0€ \0€ Ä±‡ÚÐV\ZÏPhkt&†SsZNö]žMÉC-5Rê‰²^e+K4z:)\"èu„n¹±%Öp9æyà¸]©¦rN=\Z:z+\\¢Óò.2·¤¤ü€÷ŒþŠ¤ãÞi¿/±¤^ÍGæh[ˆ8Bb¯~\\<W1j$©î—©Ðt§gxhñ8Œ®dL\ZÏq°¹Š]rœ±36ÉF9døý{Õ}Â©“è½bÐzËô8oQè•/¢ªVúòÌþWkG¿š‘hk]rhï‘`Ãt:Š a#Ú\\ÿ\0UÔñ¢¸ôFŽÉ?3|˜Ð \0€ \0€ \0€ \0€ \nö›á.¨§û±w°ë¼‹XÎß%G_Cº¬Gªäµ¤µW>z3E£ºM‰°ÌKÛ´’:§3á·zóõN0ŽÆ]º‰J{âhß+åÅLÒós˜Ì[\09•J¬žÉvw(Ç2à¸hŽŒº§!Ò‘fØßToÏ‰Ù–åéô\Zãâ—_±ÈÕj»Ï†=Rê‚\0€ \0€ \0€ \0€ \0€ \0€¯éŒÁ8/sK_·Y„{r ø*ZVüMsê‹4êgB~†GO[µÀ$ŸYÄïqÞ¦ÓÑ\n £Gu’²Y‘±S‘\0@\0@\0@ÿÙ'),('ali','1464',NULL);
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-09-01 14:52:04
