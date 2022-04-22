using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_STB
{
    class Print
    {

        public static string PrintSN(string Model,string PrintTextSn,string printCodeSn,int count,DateTime DateText,int X,int Y, string LabelScenario)
        {
            if (LabelScenario == "6")
            {
               

                return @$"
^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
^BY3,3,128^FT{89 + X},{246 + Y}^BCN,,N,N
^FD>;{printCodeSn}^FS
^FT{53 + X},{62 + Y}^A0N,33,33^FH\^FD{"GSL RAVEN"}^FS
^FT{362 + X},{62 + Y}^A0N,33,33^FH\^FD{DateText.ToString("dd.MM.yyyy HH:mm:ss")}^FS
^FT{104 + X},{108 + Y}^A0N,38,38^FH\^FD{PrintTextSn}^FS
^FT{53 + X},{305 + Y}^A0N,33,33^FH\^FDMade in Russia^FS
^FT{503 + X},{305 + Y}^A0N,33,33^FH\^FDQC Passed^FS
^FT{458 + X},{268 + Y}^A0N,25,24^FH\^FDwww.gs-labs.ru^FS
^PQ{count},0,1,Y^XZ
        ";
            }
            else
            {
                return $@"
^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
положениеиконтентсервиснаяслужба
^FO{44 + X},{185 + Y}^GFA,04352,04352,00068,:Z64:
eJzt1D9v1DAUAPBnMpgpb2UINt8AxlS4DR+Fj5CKAZ8uOlOdBBv9AsDnYEDgKBIdb2VA4KhCbJUjFg/hzHOuFCRaKIIBpHtjfPrd8/tjgG1sYxvb+C+i+XOCjxef4fpyBv4FQ4afnF3SED85w8sRoP6CUV54kpGRRc661hkwFkbuZl56nWfO6cpWDioWFVdKlRYdOPBl4eod50uo/ALyfeuAi8nIuraPB9GytfTzUAUtuPfaOOPAZLFIxpzokgytvCZDgxki5EMyim9GF222lsGM1aj3MDgdXXQQl1FgodT9sQqqBt0oH8ioIZKBycgVjnzFORld11n+WinTKKUX8prXbvCODctVMrQZb+ni1NADGX3XnholGYccqR6sW1oOZdHsqUIvFDgNg3OZzVaYC6WbPSjzkoyyJiPU4DoLeEBGoWUy5JmhCy0oj7Wim58ahygkFUGQoaAcy9lYD8GzPhnMelAegYzrXSSjswhmJCPMyIgB/FcDo0sGL0Ct9extvR8c9G2AnFEek4F8lwxDNd0YlZ+9S4b0kQz6i4Ing24tQJ3UyRgdDO2Y8qC71JOx08U+2jNjnE+G8WZjvJiMKqQ8Tuqdt27+hBpmF5s8hJaAyPfJ6CdDQ6qHeZYMf2bgLtVDpXpcX9W779z8qYX4QG7qQX2BHPnQtcdte/zVEEakvtAkJQOz59QXLXTqi1z56rG7/dQyC3yTR5oxgbxPRpqPKxpoPJJxlQDvyTjELKTeFlrRfMgjJw9dJe1ARo6sn2b9hrjJWzKmGXuori4IWQhRpjycY/0rZIHmlBquaU7xERm2QjvEjGPOPpGRy7WJR2T0H5Oxrmggd5tGyOD1QPPBIhmehGpsQukn49HG6N5LZNEDo3eMjINpX5bTvqSda8S0L2RAfLkxZKjewB0yrORW4gMfj9cyh3g3vR/JSLv/+Whp2Yhh2v3N3t6jihlFhlYCnXySfr60OBnDh2FEhObu9IbUVOLvQv/4yrhzXp7U1W/xS4P9K4b94RN9jN/LvzSy8wzal/O+/p5hLia2sY1tbOP/ii8gSQz/:A196
положениеиконтентgeneralsatellite
^FO{224 + X},{217 + Y}^GFA,02688,02688,00028,:Z64:
eJzt1M1KxDAQB/CEHPYYwatsHmFfoDT4RF4rVLOwh30LH0UKHjzuIxjpxYNgZBEjhIwzST+268dREDpdevkxZPJPs4zNNdc/qLP0PsGHfl1xwPI8MBWMd6UOYBe2zCbInAhMo3k04waTyWRgBi2QedmbJrMKjdUsliW7CtJ1ZsgaHVjorKpVb5DMBObRINnSl+OYsEZzzDy93OpntGpigFYY/35Ls5SdpS0A7y2SFXW2xYHVbB9LZlxdHFgUnb2SWRMPLCzQfGeXrjc5GOXicL0PB/gezMvernWonFfHRhNazLP6sHKVTKWoFZry2LdEu3JSj2Y1Gl8fn/doX0vlqH+29W8GPxv73mh/kZPpJhgw+6YQO3gY9h5PyUxn9cSCIINv+zBqtLfzaKAWuW9YzydzPAqLVoimUsP5YdRkIls9MYwaLYidsKbNfWk9kb5qsphsn/tGW5FdbHebaR/PUdP53W830z6Wo0aT97CZzkl3hTywxQ7upnPiHYsTA70fDD8VngwexB2uR7H160nwItuNaMn0aByazpa8PerDu5zNl/yxMS3odrTx72Suueb60/oEc4oKnw==:460E
модель
^FT{20 + X},{32 + Y}^A0N,33,33^FH\^FD{Model}^FS
дата
^FT{320 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("dd.MM.yyyy")}^FS
время
^FT{490 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("HH:mm:ss")}^FS
сн_текст
^FT{86 + X},{68 + Y}^A0N,33,36^FH\^FD{PrintTextSn}^FS
штрихкод
^BY3,3,125^FT{45 + X},{197 + Y}^BCN,,N,N
^FD>;{printCodeSn}^FS
made_QC
^FT{20 + X},{282 + Y}^A0N,33,31^FH\^FDMadeinRussia^FS
^FT{448 + X},{282 + Y}^A0N,33,33^FH\^FDQCPassed^FS
количествоэтикеток
^PQ{count},0,1,Y^XZ";
            }
            
        }

        public static string PrintID(string SC_Text,int labelCount, int X, int Y, string LabelScenario)
        {
            string text;
            if (LabelScenario == "2" || LabelScenario == "6")
                text = $@"
^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
^BY5,3,163^FT{72 + X},{217 + Y}^BCN,,N,N
^FD>;{SC_Text}^FS
^FT{82 + X},{292 + Y}^A0N,67,67^FH\^FDID^FS
^FT{159 + X},{292 + Y}^A0N,67,67^FH\^FD{SC_Text}^FS
^PQ{labelCount},0,1,Y^XZ";
            else
            {
                text = $@"
^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^JUS^LRN^CI0^XZ
^XA
^MMT
^PW650
^LL0201
^LS0
^BY5,3,93^FT{47 + X},{117 + Y}^BCN,,N,N
^FD>;{SC_Text}^FS
^FT{122 + X},{167 + Y}^A0N,54,52^FH\^FDID^FS
^FT{180 + X},{167 + Y}^A0N,54,52^FH\^FD{SC_Text}^FS
^PQ{labelCount},0,1,Y^XZ";
            }

            return text;
        }

    }
}
