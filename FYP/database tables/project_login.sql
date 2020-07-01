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
INSERT INTO `login` VALUES ('Administrator','7360','����\0JFIF\0\0\0\0\0\0\0��\0C\0	\Z\Z( \Z%!1\"%)+... 383,7(-.,��\0C\n\n\n\r\Z,$ $,,,-,,,,,,,/,,-,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,��\0\0�\0�\0��\0\0\0\0\0\0\0\0\0\0\0\0\0��\0E\0	\0\0\0\0!1AQa\"q���2BRr��#3��$Cb�����%4cs������\0\0\0\0\0\0\0\0\0\0\0\0\0��\05\0\0\0\0\0\0!12AQq\"a��B����3�CR�#4��\0\0\0?\0�(\0� U�i\r= ��\0mp���cG�Ge����¹O�)u��g�ښ��ϐ�;�Td<J�-d����K��]M}d��W ���Fj	_c�ɕP^F�jgZ��ǉ��F�\'����+���36UN����]eY%���\"��.�M��9�%�|Ŋ�:��H姃�-���$�Դ��x]��]��*�z��i餼<�j*��h|Ok�w��<��$�Q]��fu�\0@\0@\0@\0@Nt�&\nkv>M�����%N�F߆=KTџ�]\n%,�2I�K�˞s<�̮{�9eԼ�`�-G���;O�ѻu��F\rMS�69�j�&@k���`���ɔu��56��}����x����hX�򄠦�λ�x�+ l���;Xᵧ�غ�ا��88<3h�4\0� \0� \0� ��8ө����=v�	���<��[�u&����� Rg!�g�@�T����k��:fh�:��7޵�\0��0�O��Ķ۸\'2E���,����݉�ɏ��l�Vx�\03\r��?��ѓSV.�1���;x�VR0͕�����9F�d��lB�j�L�jg��r�����2`�ld���G�����Vt��<z�����t�x@\0@\0@\0@���s�\\Mi��$�mG8;ɍ\\�c�����l��z2�\Z\'�\\_�ð��w�O�bR�}l+m�[��ch�b|KGu#U�`�K����6��Fj>WBD�j]���[��s����#t����d���^��N�7�msă��>MRʪs(\Z�3�>�l�4+rH�0FeYc�����YO&�g���<��k��|E�i�d@\0@\0@\0@���!�\\.���{��\'�su�I?�����.�u(dlh�y(�NYd�ĤQ#�8�\"<���E\"�P�E��j�$MA��6J�s\rWB��psm�\\}5���E~���$�������S\0��`�ފ+�\0 ]�xQʟ���ơ\0@\0@\0@X�	����	�v��*��n��NK:I�/^	�.c\\68�.��eO��kX�H���#FS\"J��,Y`�IŐ&\n���@�(Y29v=�D����ǑW+y� �S^�5\raq\rI\0v���?N�E��\0��r�S2ɀ� \0� \0� \0��S{ñ�-=�X��$�����������w�tn�c�~��G�ˏ�wP�6ן?S~ҭ\"�>8�3(�1PȖ$	ʭ2x�\'*���@�Bɑ�Ű��7{s� �ꑲQ�o�K���p��8v�2㒒7��#�)6DК���;\\k�;�`/?���3H�l���+�sB\0� \0� \0� \0� *�C����s�focƫ�����[-����/�6�n�B�*Ñ�4�QJD�D)\\��&� ��ZL�(�;�L�\Z��Ւ����j�F�<�|�����������	f�V��}�7y��WsK�6�����T \0� \0� \0� ��q�Oێ�/��f_�?2���\Z��\\�<�P�̂e[y����m0I\"���D�,�H�(�+�-��A��FJ�g-Y*F��[��㦎���J��i�O;ZÉ\n���/̧��D�u ��F5XƆ�rU݌TV�m��HY0\0@\0@\0@���L�=����OoY�`(����Ii�ɦWij���q8��q7\'!�cy��\'ʵr7Q#�\"��t��ȴd��zՒ$y��2KW�2k�\0I�`*J�s�����P���а,*F�3q�{�n{������%UF�����Y-��)�\0� <K+Z.�\Z8�\0�(\r<�_B�gUC~O�۠3��$��TB�{�F�)7@m\0@\0@L�iL8{���]�=o��q5�8Y�ѝ]4��h�fT�X�ct�3��XɲFK��ea&����@�[�!��9�yV#Z�2�F���!h�S�ġ{��a�tRX)��7�����b������qB\0� (�U��\Zj&�&>C�q�o���9�@S1(��}C�v�9�7�DH�)�QR�Nb+��>>\nS����}�-{\0;��;ŐL�s:Y=�n����m��[xI�\r�l ��\0� \0� &\'@ٙ��֝�;���l6�J�u�r(���\'�;�3����W7v��g��\"y�{�XU��n�>��79\Z;@�*EO������S���C\\���o�1�&6�ԯ��>Wk<߆�;���H�PV7Ԩ��ϿU��A�:��Ӧ���k�����-��_0�8����y�d��\0W���WT��=ʲ�H������8�p�䈪i�2\'�Y/bs,�^��\\�Oh�e|���˴�ܖ���TXn%U֒CN�u<��UeV��g-���\'vi��+,����@������K���3G���Y42�	:f6\n�\r�`$��� �]zʹ��/���5ߦ��;��#��\\e����]�#U���=�*Zu��b�\";4�-�yE�dn��`z�־cV��.�P���PL�����\'�OUѽ��a���uH$7� ,�\0� \0� \r7�������[��Um��udЦR��Tc��s� f�������c�ruw�݌p_�m�2c�+�?���*�t�а�E�K��2n���`��axW�dɣ0/h�]&����_䣪�~�e;��T��\0�nL����Z�{�|����y��Jmp٦m���]F��o��9[5�V�d�������=��H����3;C/����uz%��~���%wN#�7N���R��,y��\Z�F\n��D��\ZѼ�8�ы��5�iu>�����s�CT�j�[�dI-$��˥��j��;�B@#,�w��u�:���5�/?�د%K���P�UT��`�)��g�����a�n}���%VU\r�<�\Zj,�嘬��H4��B�Q�#`�C�9��V��CH�cdq�1�\rh��z:\0� \0� 1�N�4����e%�e&�ʹ�M\\�c��݇����.U��O����ԡזP��$seP���4�G�e����W[�h�JO�\n��	�����z�٘�s,�[�oے�/��R\'B\\l�rv[5\n��m�$ۖ3�iA�[�.���\0ۏb���8������}J�\\cT\nX�]�2[sw7��ڤ�]��S���v\0�-afk\r��Y/\ZD����1�ύ��\r�ֺf���2����u�>}}�+g���r]�M��f]xXD��RFDR�NҊ�]1���9�k$�Ի��$�K.��M�j}�bɺ�N=Y��u\\���L�YG��.<Wf=��_�>�?�,�d�N�\0�%T� ����Gr��W$��M�3g�`5�S�i�m��4�B	��F�p�Un�.�S��SR��e����\n8�5��p�2q\'i\'n|T�[�ڔ�o�����V�\0� \0� )Zs�Q�FQ��p�w4z��xp£��m�Y�G���%vg���,�F�9[��L3��� ���kZ;��VG=�(�o�ڱ�1�$q��4�wy����2��I6������]�C]��k�Dؘ�de���w]�/T�]n��L�bR�}R�|���t���(�O���أ)��;����Z;����5�g��w<P��d|����Ö��Üܥ�]��&�2���7����=Fد��s�4����7��ҰH�0���>.ɡC�{{>?9�x�Z��G�J��E�e4�X_e³DҲ;�e���j4��]�ŭ�%��ssH�2���4��tb�\0�J�8]Ww\'���\Z9�U������B�vm�{���ؔ��,?P�p����_0L*:h����a�p\0��s�M�9c<,�kA3N���ս��y8��פ��f�ь��F��e������2�M|����O�\\�T�\0@��K�?cFC�qɣ��Gm���3hGs����|�̖Cw=�X� �Y�����l���I*)ݬ��wsZi�K	�6�/h� }Vլ�/���GYå-7נ9�1�&�l�FI�KE���{�ņ�r�~�[��<�E8?L_R�f���z���4�������iP�^_���M���]���f��j��?��H��y��r�^Uv�atG��Y4�੓I�5^5�=�Y�d��\0���{:��{/�|�K�:ٚ�٣�h��0����\'��P��ϔWX��\0?,��f�7�F^q3�ё�[�h�m豂Ѫ��+���y��[��Q�F���bZ�5���Giw�l\"��SH���O1ʪ)�x�Pe4��\0�@�wT5I,o��_��#P��dFPڞG{�e��>w]^Ɏ*o՚��f�ȸ��D \0� ]�%�S4�H�d�0w\0��3�,�A{����#�L�E�V��>-��<�6�1��9��]�.$4n.y-���DoQ\r���p[��������,�Tΰ��3]���P9�uOK#�>ۜ���{�5]�Q��<v����H�3�.�ig|����kj�ݹXv����n���	��k�)��,�gd�F˼p�2l*���?�MW�_�ɀ���Ǘ%�J+�~GL�q����bA�|�m�q/��s���m�j���~������Z[x��-i�V�源×?$���f��\"�H�ē�XȆP4Zm\\l�v��<\r�����n6]F�V��O��/癥1M��y~�h��zx�>�w�sw��r��j����}���l��A\0@\0@p�IO\'���\0�����Y~�U��*�xy$k$���\rx��<FDy-�S���\0���椧���Ds�����R�m�\Z���_�+��m]�+ӟ������\'�(r�ܽ���F	�ݐL��\\�Ѫ��\\�.�8��������L�]��p�����0�=�s�$�]aa��<5�k���K��=�oy��Ae�1����E\r�m���ĪW��h���:V�t1���~C�sf�L�X��Af��Y�/��Ч�`��jV��q�����j[�xf	�;��p{x��Z]ػ��,���y�{�C\\��2��F���uΗg�ֶYZ��I#$xt�kuG�椇f�eˎ��\ZKSR�Ϸ$z�N:f��&���O5<tq[�?_½�_���İ�<߹�Ct}��ui֍�i\'�<�����O����J��B�w����9�\0@\0@s��e`~�#i�oT�\r_���g�t���JT��P;\'7��o~N\"��	x9���)i��}Ѥ�,��T߄ω�Xy���a����G��qZ��O�~V㒗�G��4��|Ϗ���v&���{������ٙ���^��z�۽�C��%��ׯ�CIr\Z2o��U��,�j���t�vJ�Ƀ��m��,-��[Y�\n�W\'���s6 �ۖf+%V�I?�=�l�Z�2��o?\"G��<�L�\0�k��\0�����S�ԉ�G� ��l�g�����F��oݳ=�P����Ctz)\"��Re{��kOf�sE��B*o����s�q\\\"躥 � \0� ��7�E!{E�\r�8�ۮp�ʷו�S<K�\\R��Z�{G�䶏��M�2����~jZ��u�4���2�]�t��l�kx�%wa��}���Ck9ݜg����ru=���lV�����\0b��G�3�p�C�q�����j�lW$��I?�-ht_�Wݷ��Y�f���\rLĞ���w�)RQ[��^�65�A9y�H���:W��Y�7~�\\�&�\"�J������啇 ���X\"�������3Y�v���i�,�\'�XE7Lj�[��s�t��\'klx(.����\'�먥Р��S#]�[�FcyYf����\0�����]�?��R�,Jc@� \0� \0B�zA�3I!�&���v�����j���ܺj�r��S��LHi�\'s�~�>�S�͑�_r9�_�6RO�s�̹��$���g��}!�>x�+�|>���h\Zo�W�g\')9?3�Iaaf��������m���^��E��\0TR���7K��$����R���)�չ�C\r�m46ߵǉQx#�DR��XC����l=�W�.�nd��B;Q�����ե��j6��72��U�{��޷�žI���L�J쮇1�>kd�O�S�X���A����G���\Z�滔,V�vx��R�\0@\0@*�v��1�k���.<B�I�3)��\Z]��H���#�c��|>���\\��N?:z��>$Psk�{#��I��O�K���ܱ���ʴ�/�>���\'�zc��x.ʎ�ls���,��%�nn;9s+�.���Xb�I̞%i)nf�`��S�c�y�Z�0���/,��j��x������\r��gQo�D��̲3e�9��U�n��%PU�t��d�� ݃��J�kTW�AZwY�T�+$�o%sa6t�EOys㴑����iJ��a\Z)�b���VxY0L����L�bs����y\0��rڌJXYg��\naQ�݌cX;\Z\0%݌v��-��β` \0� \0� E�h�5]̌Ւ߈ޫ�����;�9���i�\r�Kk�N��S>�\n��\0Y��ۼ��v�5ZI��$�����{6Z}R�<ǟ~�\Z�	���x�ǝ�� M;�^\"��<5�����FnXp E}���s�\r��^��c�C���x)5Rp��)��o�c��@�]l�nM��CJP�_���Wcr��UҼhܸl�W���[�\0���y�濟�F�����}|WF�#��r\":q��ě��C��sr�0�ޏ�Maa����[�����i�gDG+NɢZ),μ�yH�<��[�ui�5.:�g7#~�4\0� \0� \0� ı����V\Z�Phkt&�SsZN�]�M�C-5R��^e+K4z:)\"�u�n��%�p9�y�]���rN=\Z:z+\\���.2������������i�/��^�G�h[�8Bb�~\\<W1j$��t�gxh�8��dL\Z�q����]r��36�F9d��{�}����b�z��8oQ荕/��V����WkG����hk]rh�`�t:� a#�\\�\0U���F��?3|�� \0� \0� \0� \0� \n���.����w����X���%G_C��G�䵤�W>z3E��M���K۴�:�3�z��N0��]��J{�h�+��L��s��[�\09�J����vw(�2�h����!ґf��Toωٖ���\Z��_���j�φ=R��\0� \0� \0� \0� \0� \0�����8/sK_�Y�{r �*Z�V�Ms�4�gB~�GO[��$�Y��qަ��\n��Gu��Y��S�\0@\0@\0@��'),('ali','1464',NULL);
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
