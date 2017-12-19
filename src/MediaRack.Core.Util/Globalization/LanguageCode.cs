using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Globalization
{
    public sealed class LanguageCode
    {
        #region LANG_CODES

        public static readonly List<LanguageCode> ALL_CODES = new List<LanguageCode>(){
                new LanguageCode(){ EnglishName = "Abkhazian",NativeName = "аҧсуа бызшәа, аҧсшәа",ISO639_1 = "ab",ISO639_2T = "abk",ISO639_2B = "abk",ISO639_3 = "abk"},
                new LanguageCode(){ EnglishName = "Afar",NativeName = "Afaraf",ISO639_1 = "aa",ISO639_2T = "aar",ISO639_2B = "aar",ISO639_3 = "aar"},
                new LanguageCode(){ EnglishName = "Afrikaans",NativeName = "Afrikaans",ISO639_1 = "af",ISO639_2T = "afr",ISO639_2B = "afr",ISO639_3 = "afr"},
                new LanguageCode(){ EnglishName = "Akan",NativeName = "Akan",ISO639_1 = "ak",ISO639_2T = "aka",ISO639_2B = "aka",ISO639_3 = "aka"},
                new LanguageCode(){ EnglishName = "Albanian",NativeName = "Shqip",ISO639_1 = "sq",ISO639_2T = "sqi",ISO639_2B = "alb",ISO639_3 = "sqi"},
                new LanguageCode(){ EnglishName = "Amharic",NativeName = "አማርኛ",ISO639_1 = "am",ISO639_2T = "amh",ISO639_2B = "amh",ISO639_3 = "amh"},
                new LanguageCode(){ EnglishName = "Arabic",NativeName = "العربية",ISO639_1 = "ar",ISO639_2T = "ara",ISO639_2B = "ara",ISO639_3 = "ara"},
                new LanguageCode(){ EnglishName = "Aragonese",NativeName = "aragonés",ISO639_1 = "an",ISO639_2T = "arg",ISO639_2B = "arg",ISO639_3 = "arg"},
                new LanguageCode(){ EnglishName = "Armenian",NativeName = "Հայերեն",ISO639_1 = "hy",ISO639_2T = "hye",ISO639_2B = "arm",ISO639_3 = "hye"},
                new LanguageCode(){ EnglishName = "Assamese",NativeName = "অসমীয়া",ISO639_1 = "as",ISO639_2T = "asm",ISO639_2B = "asm",ISO639_3 = "asm"},
                new LanguageCode(){ EnglishName = "Avaric",NativeName = "авар мацӀ, магӀарул мацӀ",ISO639_1 = "av",ISO639_2T = "ava",ISO639_2B = "ava",ISO639_3 = "ava"},
                new LanguageCode(){ EnglishName = "Avestan",NativeName = "avesta",ISO639_1 = "ae",ISO639_2T = "ave",ISO639_2B = "ave",ISO639_3 = "ave"},
                new LanguageCode(){ EnglishName = "Aymara",NativeName = "aymar aru",ISO639_1 = "ay",ISO639_2T = "aym",ISO639_2B = "aym",ISO639_3 = "aym"},
                new LanguageCode(){ EnglishName = "Azerbaijani",NativeName = "azərbaycan dili",ISO639_1 = "az",ISO639_2T = "aze",ISO639_2B = "aze",ISO639_3 = "aze"},
                new LanguageCode(){ EnglishName = "Bambara",NativeName = "bamanankan",ISO639_1 = "bm",ISO639_2T = "bam",ISO639_2B = "bam",ISO639_3 = "bam"},
                new LanguageCode(){ EnglishName = "Bashkir",NativeName = "башҡорт теле",ISO639_1 = "ba",ISO639_2T = "bak",ISO639_2B = "bak",ISO639_3 = "bak"},
                new LanguageCode(){ EnglishName = "Basque",NativeName = "euskara, euskera",ISO639_1 = "eu",ISO639_2T = "eus",ISO639_2B = "baq",ISO639_3 = "eus"},
                new LanguageCode(){ EnglishName = "Belarusian",NativeName = "беларуская мова",ISO639_1 = "be",ISO639_2T = "bel",ISO639_2B = "bel",ISO639_3 = "bel"},
                new LanguageCode(){ EnglishName = "Bengali",NativeName = "বাংলা",ISO639_1 = "bn",ISO639_2T = "ben",ISO639_2B = "ben",ISO639_3 = "ben"},
                new LanguageCode(){ EnglishName = "Bihari",NativeName = "भोजपुरी",ISO639_1 = "bh",ISO639_2T = "bih",ISO639_2B = "bih",ISO639_3 = ""},
                new LanguageCode(){ EnglishName = "Bislama",NativeName = "Bislama",ISO639_1 = "bi",ISO639_2T = "bis",ISO639_2B = "bis",ISO639_3 = "bis"},
                new LanguageCode(){ EnglishName = "Bosnian",NativeName = "bosanski jezik",ISO639_1 = "bs",ISO639_2T = "bos",ISO639_2B = "bos",ISO639_3 = "bos"},
                new LanguageCode(){ EnglishName = "Breton",NativeName = "brezhoneg",ISO639_1 = "br",ISO639_2T = "bre",ISO639_2B = "bre",ISO639_3 = "bre"},
                new LanguageCode(){ EnglishName = "Bulgarian",NativeName = "български език",ISO639_1 = "bg",ISO639_2T = "bul",ISO639_2B = "bul",ISO639_3 = "bul"},
                new LanguageCode(){ EnglishName = "Burmese",NativeName = "ဗမာစာ",ISO639_1 = "my",ISO639_2T = "mya",ISO639_2B = "bur",ISO639_3 = "mya"},
                new LanguageCode(){ EnglishName = "Catalan",NativeName = "català, valencià",ISO639_1 = "ca",ISO639_2T = "cat",ISO639_2B = "cat",ISO639_3 = "cat"},
                new LanguageCode(){ EnglishName = "Chamorro",NativeName = "Chamoru",ISO639_1 = "ch",ISO639_2T = "cha",ISO639_2B = "cha",ISO639_3 = "cha"},
                new LanguageCode(){ EnglishName = "Chechen",NativeName = "нохчийн мотт",ISO639_1 = "ce",ISO639_2T = "che",ISO639_2B = "che",ISO639_3 = "che"},
                new LanguageCode(){ EnglishName = "Chichewa",NativeName = "chiCheŵa, chinyanja",ISO639_1 = "ny",ISO639_2T = "nya",ISO639_2B = "nya",ISO639_3 = "nya"},
                new LanguageCode(){ EnglishName = "Chinese",NativeName = "中文 (Zhōngwén), 汉语, 漢語",ISO639_1 = "zh",ISO639_2T = "zho",ISO639_2B = "chi",ISO639_3 = "zho"},
                new LanguageCode(){ EnglishName = "Chuvash",NativeName = "чӑваш чӗлхи",ISO639_1 = "cv",ISO639_2T = "chv",ISO639_2B = "chv",ISO639_3 = "chv"},
                new LanguageCode(){ EnglishName = "Cornish",NativeName = "Kernewek",ISO639_1 = "kw",ISO639_2T = "cor",ISO639_2B = "cor",ISO639_3 = "cor"},
                new LanguageCode(){ EnglishName = "Corsican",NativeName = "corsu, lingua corsa",ISO639_1 = "co",ISO639_2T = "cos",ISO639_2B = "cos",ISO639_3 = "cos"},
                new LanguageCode(){ EnglishName = "Cree",NativeName = "ᓀᐦᐃᔭᐍᐏᐣ",ISO639_1 = "cr",ISO639_2T = "cre",ISO639_2B = "cre",ISO639_3 = "cre"},
                new LanguageCode(){ EnglishName = "Croatian",NativeName = "hrvatski jezik",ISO639_1 = "hr",ISO639_2T = "hrv",ISO639_2B = "hrv",ISO639_3 = "hrv"},
                new LanguageCode(){ EnglishName = "Czech",NativeName = "čeština, český jazyk",ISO639_1 = "cs",ISO639_2T = "ces",ISO639_2B = "cze",ISO639_3 = "ces"},
                new LanguageCode(){ EnglishName = "Danish",NativeName = "dansk",ISO639_1 = "da",ISO639_2T = "dan",ISO639_2B = "dan",ISO639_3 = "dan"},
                new LanguageCode(){ EnglishName = "Divehi",NativeName = "ދިވެހި",ISO639_1 = "dv",ISO639_2T = "div",ISO639_2B = "div",ISO639_3 = "div"},
                new LanguageCode(){ EnglishName = "Dutch",NativeName = "Nederlands, Vlaams",ISO639_1 = "nl",ISO639_2T = "nld",ISO639_2B = "dut",ISO639_3 = "nld"},
                new LanguageCode(){ EnglishName = "Dzongkha",NativeName = "རྫོང་ཁ",ISO639_1 = "dz",ISO639_2T = "dzo",ISO639_2B = "dzo",ISO639_3 = "dzo"},
                new LanguageCode(){ EnglishName = "English",NativeName = "English",ISO639_1 = "en",ISO639_2T = "eng",ISO639_2B = "eng",ISO639_3 = "eng"},
                new LanguageCode(){ EnglishName = "Esperanto",NativeName = "Esperanto",ISO639_1 = "eo",ISO639_2T = "epo",ISO639_2B = "epo",ISO639_3 = "epo"},
                new LanguageCode(){ EnglishName = "Estonian",NativeName = "eesti, eesti keel",ISO639_1 = "et",ISO639_2T = "est",ISO639_2B = "est",ISO639_3 = "est"},
                new LanguageCode(){ EnglishName = "Ewe",NativeName = "Eʋegbe",ISO639_1 = "ee",ISO639_2T = "ewe",ISO639_2B = "ewe",ISO639_3 = "ewe"},
                new LanguageCode(){ EnglishName = "Faroese",NativeName = "føroyskt",ISO639_1 = "fo",ISO639_2T = "fao",ISO639_2B = "fao",ISO639_3 = "fao"},
                new LanguageCode(){ EnglishName = "Fijian",NativeName = "vosa Vakaviti",ISO639_1 = "fj",ISO639_2T = "fij",ISO639_2B = "fij",ISO639_3 = "fij"},
                new LanguageCode(){ EnglishName = "Finnish",NativeName = "suomi, suomen kieli",ISO639_1 = "fi",ISO639_2T = "fin",ISO639_2B = "fin",ISO639_3 = "fin"},
                new LanguageCode(){ EnglishName = "French",NativeName = "français, langue française",ISO639_1 = "fr",ISO639_2T = "fra",ISO639_2B = "fre",ISO639_3 = "fra"},
                new LanguageCode(){ EnglishName = "Fulah",NativeName = "Fulfulde, Pulaar, Pular",ISO639_1 = "ff",ISO639_2T = "ful",ISO639_2B = "ful",ISO639_3 = "ful"},
                new LanguageCode(){ EnglishName = "Galician",NativeName = "Galego",ISO639_1 = "gl",ISO639_2T = "glg",ISO639_2B = "glg",ISO639_3 = "glg"},
                new LanguageCode(){ EnglishName = "Georgian",NativeName = "ქართული",ISO639_1 = "ka",ISO639_2T = "kat",ISO639_2B = "geo",ISO639_3 = "kat"},
                new LanguageCode(){ EnglishName = "German",NativeName = "Deutsch",ISO639_1 = "de",ISO639_2T = "deu",ISO639_2B = "ger",ISO639_3 = "deu"},
                new LanguageCode(){ EnglishName = "Greek",NativeName = "ελληνικά",ISO639_1 = "el",ISO639_2T = "ell",ISO639_2B = "gre",ISO639_3 = "ell"},
                new LanguageCode(){ EnglishName = "Guaraní",NativeName = "Avañe'ẽ",ISO639_1 = "gn",ISO639_2T = "grn",ISO639_2B = "grn",ISO639_3 = "grn"},
                new LanguageCode(){ EnglishName = "Gujarati",NativeName = "ગુજરાતી",ISO639_1 = "gu",ISO639_2T = "guj",ISO639_2B = "guj",ISO639_3 = "guj"},
                new LanguageCode(){ EnglishName = "Haitian",NativeName = "Kreyòl ayisyen",ISO639_1 = "ht",ISO639_2T = "hat",ISO639_2B = "hat",ISO639_3 = "hat"},
                new LanguageCode(){ EnglishName = "Hausa",NativeName = "(Hausa) هَوُسَ",ISO639_1 = "ha",ISO639_2T = "hau",ISO639_2B = "hau",ISO639_3 = "hau"},
                new LanguageCode(){ EnglishName = "Hebrew",NativeName = "עברית",ISO639_1 = "he",ISO639_2T = "heb",ISO639_2B = "heb",ISO639_3 = "heb"},
                new LanguageCode(){ EnglishName = "Herero",NativeName = "Otjiherero",ISO639_1 = "hz",ISO639_2T = "her",ISO639_2B = "her",ISO639_3 = "her"},
                new LanguageCode(){ EnglishName = "Hindi",NativeName = "हिन्दी, हिंदी",ISO639_1 = "hi",ISO639_2T = "hin",ISO639_2B = "hin",ISO639_3 = "hin"},
                new LanguageCode(){ EnglishName = "Hiri Motu",NativeName = "Hiri Motu",ISO639_1 = "ho",ISO639_2T = "hmo",ISO639_2B = "hmo",ISO639_3 = "hmo"},
                new LanguageCode(){ EnglishName = "Hungarian",NativeName = "magyar",ISO639_1 = "hu",ISO639_2T = "hun",ISO639_2B = "hun",ISO639_3 = "hun"},
                new LanguageCode(){ EnglishName = "Interlingua",NativeName = "Interlingua",ISO639_1 = "ia",ISO639_2T = "ina",ISO639_2B = "ina",ISO639_3 = "ina"},
                new LanguageCode(){ EnglishName = "Indonesian",NativeName = "Bahasa Indonesia",ISO639_1 = "id",ISO639_2T = "ind",ISO639_2B = "ind",ISO639_3 = "ind"},
                new LanguageCode(){ EnglishName = "Interlingue",NativeName = "Originally called Occidental; then Interlingue after WWII",ISO639_1 = "ie",ISO639_2T = "ile",ISO639_2B = "ile",ISO639_3 = "ile"},
                new LanguageCode(){ EnglishName = "Irish",NativeName = "Gaeilge",ISO639_1 = "ga",ISO639_2T = "gle",ISO639_2B = "gle",ISO639_3 = "gle"},
                new LanguageCode(){ EnglishName = "Igbo",NativeName = "Asụsụ Igbo",ISO639_1 = "ig",ISO639_2T = "ibo",ISO639_2B = "ibo",ISO639_3 = "ibo"},
                new LanguageCode(){ EnglishName = "Inupiaq",NativeName = "Iñupiaq, Iñupiatun",ISO639_1 = "ik",ISO639_2T = "ipk",ISO639_2B = "ipk",ISO639_3 = "ipk"},
                new LanguageCode(){ EnglishName = "Ido",NativeName = "Ido",ISO639_1 = "io",ISO639_2T = "ido",ISO639_2B = "ido",ISO639_3 = "ido"},
                new LanguageCode(){ EnglishName = "Icelandic",NativeName = "Íslenska",ISO639_1 = "is",ISO639_2T = "isl",ISO639_2B = "ice",ISO639_3 = "isl"},
                new LanguageCode(){ EnglishName = "Italian",NativeName = "Italiano",ISO639_1 = "it",ISO639_2T = "ita",ISO639_2B = "ita",ISO639_3 = "ita"},
                new LanguageCode(){ EnglishName = "Inuktitut",NativeName = "ᐃᓄᒃᑎᑐᑦ",ISO639_1 = "iu",ISO639_2T = "iku",ISO639_2B = "iku",ISO639_3 = "iku"},
                new LanguageCode(){ EnglishName = "Japanese",NativeName = "日本語 (にほんご)",ISO639_1 = "ja",ISO639_2T = "jpn",ISO639_2B = "jpn",ISO639_3 = "jpn"},
                new LanguageCode(){ EnglishName = "Javanese",NativeName = "ꦧꦱꦗꦮ, Basa Jawa",ISO639_1 = "jv",ISO639_2T = "jav",ISO639_2B = "jav",ISO639_3 = "jav"},
                new LanguageCode(){ EnglishName = "Kalaallisut, Greenlandic",NativeName = "kalaallisut, kalaallit oqaasii",ISO639_1 = "kl",ISO639_2T = "kal",ISO639_2B = "kal",ISO639_3 = "kal"},
                new LanguageCode(){ EnglishName = "Kannada",NativeName = "ಕನ್ನಡ",ISO639_1 = "kn",ISO639_2T = "kan",ISO639_2B = "kan",ISO639_3 = "kan"},
                new LanguageCode(){ EnglishName = "Kanuri",NativeName = "Kanuri",ISO639_1 = "kr",ISO639_2T = "kau",ISO639_2B = "kau",ISO639_3 = "kau"},
                new LanguageCode(){ EnglishName = "Kashmiri",NativeName = "कश्मीरी, كشميري‎",ISO639_1 = "ks",ISO639_2T = "kas",ISO639_2B = "kas",ISO639_3 = "kas"},
                new LanguageCode(){ EnglishName = "Kazakh",NativeName = "қазақ тілі",ISO639_1 = "kk",ISO639_2T = "kaz",ISO639_2B = "kaz",ISO639_3 = "kaz"},
                new LanguageCode(){ EnglishName = "Central Khmer",NativeName = "ខ្មែរ, ខេមរភាសា, ភាសាខ្មែរ",ISO639_1 = "km",ISO639_2T = "khm",ISO639_2B = "khm",ISO639_3 = "khm"},
                new LanguageCode(){ EnglishName = "Kikuyu",NativeName = "Gĩkũyũ",ISO639_1 = "ki",ISO639_2T = "kik",ISO639_2B = "kik",ISO639_3 = "kik"},
                new LanguageCode(){ EnglishName = "Kinyarwanda",NativeName = "Ikinyarwanda",ISO639_1 = "rw",ISO639_2T = "kin",ISO639_2B = "kin",ISO639_3 = "kin"},
                new LanguageCode(){ EnglishName = "Kirghiz",NativeName = "Кыргызча, Кыргыз тили",ISO639_1 = "ky",ISO639_2T = "kir",ISO639_2B = "kir",ISO639_3 = "kir"},
                new LanguageCode(){ EnglishName = "Komi",NativeName = "коми кыв",ISO639_1 = "kv",ISO639_2T = "kom",ISO639_2B = "kom",ISO639_3 = "kom"},
                new LanguageCode(){ EnglishName = "Kongo",NativeName = "Kikongo",ISO639_1 = "kg",ISO639_2T = "kon",ISO639_2B = "kon",ISO639_3 = "kon"},
                new LanguageCode(){ EnglishName = "Korean",NativeName = "한국어",ISO639_1 = "ko",ISO639_2T = "kor",ISO639_2B = "kor",ISO639_3 = "kor"},
                new LanguageCode(){ EnglishName = "Kurdish",NativeName = "Kurdî, كوردی‎",ISO639_1 = "ku",ISO639_2T = "kur",ISO639_2B = "kur",ISO639_3 = "kur"},
                new LanguageCode(){ EnglishName = "Kuanyama",NativeName = "Kuanyama",ISO639_1 = "kj",ISO639_2T = "kua",ISO639_2B = "kua",ISO639_3 = "kua"},
                new LanguageCode(){ EnglishName = "Latin",NativeName = "latine, lingua latina",ISO639_1 = "la",ISO639_2T = "lat",ISO639_2B = "lat",ISO639_3 = "lat"},
                new LanguageCode(){ EnglishName = "Luxembourgish",NativeName = "Lëtzebuergesch",ISO639_1 = "lb",ISO639_2T = "ltz",ISO639_2B = "ltz",ISO639_3 = "ltz"},
                new LanguageCode(){ EnglishName = "Ganda",NativeName = "Luganda",ISO639_1 = "lg",ISO639_2T = "lug",ISO639_2B = "lug",ISO639_3 = "lug"},
                new LanguageCode(){ EnglishName = "Limburgan",NativeName = "Limburgs",ISO639_1 = "li",ISO639_2T = "lim",ISO639_2B = "lim",ISO639_3 = "lim"},
                new LanguageCode(){ EnglishName = "Lingala",NativeName = "Lingála",ISO639_1 = "ln",ISO639_2T = "lin",ISO639_2B = "lin",ISO639_3 = "lin"},
                new LanguageCode(){ EnglishName = "Lao",NativeName = "ພາສາລາວ",ISO639_1 = "lo",ISO639_2T = "lao",ISO639_2B = "lao",ISO639_3 = "lao"},
                new LanguageCode(){ EnglishName = "Lithuanian",NativeName = "lietuvių kalba",ISO639_1 = "lt",ISO639_2T = "lit",ISO639_2B = "lit",ISO639_3 = "lit"},
                new LanguageCode(){ EnglishName = "Luba-Katanga",NativeName = "Kiluba",ISO639_1 = "lu",ISO639_2T = "lub",ISO639_2B = "lub",ISO639_3 = "lub"},
                new LanguageCode(){ EnglishName = "Latvian",NativeName = "Latviešu Valoda",ISO639_1 = "lv",ISO639_2T = "lav",ISO639_2B = "lav",ISO639_3 = "lav"},
                new LanguageCode(){ EnglishName = "Manx",NativeName = "Gaelg, Gailck",ISO639_1 = "gv",ISO639_2T = "glv",ISO639_2B = "glv",ISO639_3 = "glv"},
                new LanguageCode(){ EnglishName = "Macedonian",NativeName = "македонски јазик",ISO639_1 = "mk",ISO639_2T = "mkd",ISO639_2B = "mac",ISO639_3 = "mkd"},
                new LanguageCode(){ EnglishName = "Malagasy",NativeName = "fiteny malagasy",ISO639_1 = "mg",ISO639_2T = "mlg",ISO639_2B = "mlg",ISO639_3 = "mlg"},
                new LanguageCode(){ EnglishName = "Malay",NativeName = "Bahasa Melayu, بهاس ملايو‎",ISO639_1 = "ms",ISO639_2T = "msa",ISO639_2B = "may",ISO639_3 = "msa"},
                new LanguageCode(){ EnglishName = "Malayalam",NativeName = "മലയാളം",ISO639_1 = "ml",ISO639_2T = "mal",ISO639_2B = "mal",ISO639_3 = "mal"},
                new LanguageCode(){ EnglishName = "Maltese",NativeName = "Malti",ISO639_1 = "mt",ISO639_2T = "mlt",ISO639_2B = "mlt",ISO639_3 = "mlt"},
                new LanguageCode(){ EnglishName = "Maori",NativeName = "te reo Māori",ISO639_1 = "mi",ISO639_2T = "mri",ISO639_2B = "mao",ISO639_3 = "mri"},
                new LanguageCode(){ EnglishName = "Marathi",NativeName = "मराठी",ISO639_1 = "mr",ISO639_2T = "mar",ISO639_2B = "mar",ISO639_3 = "mar"},
                new LanguageCode(){ EnglishName = "Marshallese",NativeName = "Kajin M̧ajeļ",ISO639_1 = "mh",ISO639_2T = "mah",ISO639_2B = "mah",ISO639_3 = "mah"},
                new LanguageCode(){ EnglishName = "Mongolian",NativeName = "Монгол хэл",ISO639_1 = "mn",ISO639_2T = "mon",ISO639_2B = "mon",ISO639_3 = "mon"},
                new LanguageCode(){ EnglishName = "Nauru",NativeName = "Dorerin Naoero",ISO639_1 = "na",ISO639_2T = "nau",ISO639_2B = "nau",ISO639_3 = "nau"},
                new LanguageCode(){ EnglishName = "Navajo, Navaho",NativeName = "Diné bizaad",ISO639_1 = "nv",ISO639_2T = "nav",ISO639_2B = "nav",ISO639_3 = "nav"},
                new LanguageCode(){ EnglishName = "North Ndebele",NativeName = "isiNdebele",ISO639_1 = "nd",ISO639_2T = "nde",ISO639_2B = "nde",ISO639_3 = "nde"},
                new LanguageCode(){ EnglishName = "Nepali",NativeName = "नेपाली",ISO639_1 = "ne",ISO639_2T = "nep",ISO639_2B = "nep",ISO639_3 = "nep"},
                new LanguageCode(){ EnglishName = "Ndonga",NativeName = "Owambo",ISO639_1 = "ng",ISO639_2T = "ndo",ISO639_2B = "ndo",ISO639_3 = "ndo"},
                new LanguageCode(){ EnglishName = "Norwegian Bokmål",NativeName = "Norsk Bokmål",ISO639_1 = "nb",ISO639_2T = "nob",ISO639_2B = "nob",ISO639_3 = "nob"},
                new LanguageCode(){ EnglishName = "Norwegian Nynorsk",NativeName = "Norsk Nynorsk",ISO639_1 = "nn",ISO639_2T = "nno",ISO639_2B = "nno",ISO639_3 = "nno"},
                new LanguageCode(){ EnglishName = "Norwegian",NativeName = "Norsk",ISO639_1 = "no",ISO639_2T = "nor",ISO639_2B = "nor",ISO639_3 = "nor"},
                new LanguageCode(){ EnglishName = "Sichuan Yi",NativeName = "ꆈꌠ꒿ Nuosuhxop",ISO639_1 = "ii",ISO639_2T = "iii",ISO639_2B = "iii",ISO639_3 = "iii"},
                new LanguageCode(){ EnglishName = "South Ndebele",NativeName = "isiNdebele",ISO639_1 = "nr",ISO639_2T = "nbl",ISO639_2B = "nbl",ISO639_3 = "nbl"},
                new LanguageCode(){ EnglishName = "Occitan",NativeName = "occitan, lenga d'òc",ISO639_1 = "oc",ISO639_2T = "oci",ISO639_2B = "oci",ISO639_3 = "oci"},
                new LanguageCode(){ EnglishName = "Ojibwa",NativeName = "ᐊᓂᔑᓈᐯᒧᐎᓐ",ISO639_1 = "oj",ISO639_2T = "oji",ISO639_2B = "oji",ISO639_3 = "oji"},
                new LanguageCode(){ EnglishName = "Church Slavic",NativeName = "ѩзыкъ словѣньскъ",ISO639_1 = "cu",ISO639_2T = "chu",ISO639_2B = "chu",ISO639_3 = "chu"},
                new LanguageCode(){ EnglishName = "Oromo",NativeName = "Afaan Oromoo",ISO639_1 = "om",ISO639_2T = "orm",ISO639_2B = "orm",ISO639_3 = "orm"},
                new LanguageCode(){ EnglishName = "Oriya",NativeName = "ଓଡ଼ିଆ",ISO639_1 = "or",ISO639_2T = "ori",ISO639_2B = "ori",ISO639_3 = "ori"},
                new LanguageCode(){ EnglishName = "Ossetian",NativeName = "ирон æвзаг",ISO639_1 = "os",ISO639_2T = "oss",ISO639_2B = "oss",ISO639_3 = "oss"},
                new LanguageCode(){ EnglishName = "Panjabi",NativeName = "ਪੰਜਾਬੀ",ISO639_1 = "pa",ISO639_2T = "pan",ISO639_2B = "pan",ISO639_3 = "pan"},
                new LanguageCode(){ EnglishName = "Pali",NativeName = "पाऴि",ISO639_1 = "pi",ISO639_2T = "pli",ISO639_2B = "pli",ISO639_3 = "pli"},
                new LanguageCode(){ EnglishName = "Persian",NativeName = "فارسی",ISO639_1 = "fa",ISO639_2T = "fas",ISO639_2B = "per",ISO639_3 = "fas"},
                new LanguageCode(){ EnglishName = "Polish",NativeName = "Język Polski, Polszczyzna",ISO639_1 = "pl",ISO639_2T = "pol",ISO639_2B = "pol",ISO639_3 = "pol"},
                new LanguageCode(){ EnglishName = "Pashto, Pushto",NativeName = "پښتو",ISO639_1 = "ps",ISO639_2T = "pus",ISO639_2B = "pus",ISO639_3 = "pus"},
                new LanguageCode(){ EnglishName = "Portuguese",NativeName = "Português",ISO639_1 = "pt",ISO639_2T = "por",ISO639_2B = "por",ISO639_3 = "por"},
                new LanguageCode(){ EnglishName = "Quechua",NativeName = "Runa Simi, Kichwa",ISO639_1 = "qu",ISO639_2T = "que",ISO639_2B = "que",ISO639_3 = "que"},
                new LanguageCode(){ EnglishName = "Romansh",NativeName = "Rumantsch Grischun",ISO639_1 = "rm",ISO639_2T = "roh",ISO639_2B = "roh",ISO639_3 = "roh"},
                new LanguageCode(){ EnglishName = "Rundi",NativeName = "Ikirundi",ISO639_1 = "rn",ISO639_2T = "run",ISO639_2B = "run",ISO639_3 = "run"},
                new LanguageCode(){ EnglishName = "Romanian",NativeName = "Română",ISO639_1 = "ro",ISO639_2T = "ron",ISO639_2B = "rum",ISO639_3 = "ron"},
                new LanguageCode(){ EnglishName = "Russian",NativeName = "Русский",ISO639_1 = "ru",ISO639_2T = "rus",ISO639_2B = "rus",ISO639_3 = "rus"},
                new LanguageCode(){ EnglishName = "Sanskrit",NativeName = "संस्कृतम्",ISO639_1 = "sa",ISO639_2T = "san",ISO639_2B = "san",ISO639_3 = "san"},
                new LanguageCode(){ EnglishName = "Sardinian",NativeName = "sardu",ISO639_1 = "sc",ISO639_2T = "srd",ISO639_2B = "srd",ISO639_3 = "srd"},
                new LanguageCode(){ EnglishName = "Sindhi",NativeName = "सिन्धी, سنڌي، سندھی‎",ISO639_1 = "sd",ISO639_2T = "snd",ISO639_2B = "snd",ISO639_3 = "snd"},
                new LanguageCode(){ EnglishName = "Northern Sami",NativeName = "Davvisámegiella",ISO639_1 = "se",ISO639_2T = "sme",ISO639_2B = "sme",ISO639_3 = "sme"},
                new LanguageCode(){ EnglishName = "Samoan",NativeName = "gagana fa'a Samoa",ISO639_1 = "sm",ISO639_2T = "smo",ISO639_2B = "smo",ISO639_3 = "smo"},
                new LanguageCode(){ EnglishName = "Sango",NativeName = "yângâ tî sängö",ISO639_1 = "sg",ISO639_2T = "sag",ISO639_2B = "sag",ISO639_3 = "sag"},
                new LanguageCode(){ EnglishName = "Serbian",NativeName = "српски језик",ISO639_1 = "sr",ISO639_2T = "srp",ISO639_2B = "srp",ISO639_3 = "srp"},
                new LanguageCode(){ EnglishName = "Gaelic",NativeName = "Gàidhlig",ISO639_1 = "gd",ISO639_2T = "gla",ISO639_2B = "gla",ISO639_3 = "gla"},
                new LanguageCode(){ EnglishName = "Shona",NativeName = "chiShona",ISO639_1 = "sn",ISO639_2T = "sna",ISO639_2B = "sna",ISO639_3 = "sna"},
                new LanguageCode(){ EnglishName = "Sinhala",NativeName = "සිංහල",ISO639_1 = "si",ISO639_2T = "sin",ISO639_2B = "sin",ISO639_3 = "sin"},
                new LanguageCode(){ EnglishName = "Slovak",NativeName = "Slovenčina, Slovenský Jazyk",ISO639_1 = "sk",ISO639_2T = "slk",ISO639_2B = "slo",ISO639_3 = "slk"},
                new LanguageCode(){ EnglishName = "Slovenian",NativeName = "Slovenski Jezik, Slovenščina",ISO639_1 = "sl",ISO639_2T = "slv",ISO639_2B = "slv",ISO639_3 = "slv"},
                new LanguageCode(){ EnglishName = "Somali",NativeName = "Soomaaliga, af Soomaali",ISO639_1 = "so",ISO639_2T = "som",ISO639_2B = "som",ISO639_3 = "som"},
                new LanguageCode(){ EnglishName = "Southern Sotho",NativeName = "Sesotho",ISO639_1 = "st",ISO639_2T = "sot",ISO639_2B = "sot",ISO639_3 = "sot"},
                new LanguageCode(){ EnglishName = "Spanish",NativeName = "Español",ISO639_1 = "es",ISO639_2T = "spa",ISO639_2B = "spa",ISO639_3 = "spa"},
                new LanguageCode(){ EnglishName = "Sundanese",NativeName = "Basa Sunda",ISO639_1 = "su",ISO639_2T = "sun",ISO639_2B = "sun",ISO639_3 = "sun"},
                new LanguageCode(){ EnglishName = "Swahili",NativeName = "Kiswahili",ISO639_1 = "sw",ISO639_2T = "swa",ISO639_2B = "swa",ISO639_3 = "swa"},
                new LanguageCode(){ EnglishName = "Swati",NativeName = "SiSwati",ISO639_1 = "ss",ISO639_2T = "ssw",ISO639_2B = "ssw",ISO639_3 = "ssw"},
                new LanguageCode(){ EnglishName = "Swedish",NativeName = "Svenska",ISO639_1 = "sv",ISO639_2T = "swe",ISO639_2B = "swe",ISO639_3 = "swe"},
                new LanguageCode(){ EnglishName = "Tamil",NativeName = "தமிழ்",ISO639_1 = "ta",ISO639_2T = "tam",ISO639_2B = "tam",ISO639_3 = "tam"},
                new LanguageCode(){ EnglishName = "Telugu",NativeName = "తెలుగు",ISO639_1 = "te",ISO639_2T = "tel",ISO639_2B = "tel",ISO639_3 = "tel"},
                new LanguageCode(){ EnglishName = "Tajik",NativeName = "тоҷикӣ, toçikī, تاجیکی‎",ISO639_1 = "tg",ISO639_2T = "tgk",ISO639_2B = "tgk",ISO639_3 = "tgk"},
                new LanguageCode(){ EnglishName = "Thai",NativeName = "ไทย",ISO639_1 = "th",ISO639_2T = "tha",ISO639_2B = "tha",ISO639_3 = "tha"},
                new LanguageCode(){ EnglishName = "Tigrinya",NativeName = "ትግርኛ",ISO639_1 = "ti",ISO639_2T = "tir",ISO639_2B = "tir",ISO639_3 = "tir"},
                new LanguageCode(){ EnglishName = "Tibetan",NativeName = "བོད་ཡིག",ISO639_1 = "bo",ISO639_2T = "bod",ISO639_2B = "tib",ISO639_3 = "bod"},
                new LanguageCode(){ EnglishName = "Turkmen",NativeName = "Türkmen, Түркмен",ISO639_1 = "tk",ISO639_2T = "tuk",ISO639_2B = "tuk",ISO639_3 = "tuk"},
                new LanguageCode(){ EnglishName = "Tagalog",NativeName = "Wikang Tagalog",ISO639_1 = "tl",ISO639_2T = "tgl",ISO639_2B = "tgl",ISO639_3 = "tgl"},
                new LanguageCode(){ EnglishName = "Tswana",NativeName = "Setswana",ISO639_1 = "tn",ISO639_2T = "tsn",ISO639_2B = "tsn",ISO639_3 = "tsn"},
                new LanguageCode(){ EnglishName = "Tonga",NativeName = "Faka Tonga",ISO639_1 = "to",ISO639_2T = "ton",ISO639_2B = "ton",ISO639_3 = "ton"},
                new LanguageCode(){ EnglishName = "Turkish",NativeName = "Türkçe",ISO639_1 = "tr",ISO639_2T = "tur",ISO639_2B = "tur",ISO639_3 = "tur"},
                new LanguageCode(){ EnglishName = "Tsonga",NativeName = "Xitsonga",ISO639_1 = "ts",ISO639_2T = "tso",ISO639_2B = "tso",ISO639_3 = "tso"},
                new LanguageCode(){ EnglishName = "Tatar",NativeName = "татар теле, tatar tele",ISO639_1 = "tt",ISO639_2T = "tat",ISO639_2B = "tat",ISO639_3 = "tat"},
                new LanguageCode(){ EnglishName = "Twi",NativeName = "Twi",ISO639_1 = "tw",ISO639_2T = "twi",ISO639_2B = "twi",ISO639_3 = "twi"},
                new LanguageCode(){ EnglishName = "Tahitian",NativeName = "Reo Tahiti",ISO639_1 = "ty",ISO639_2T = "tah",ISO639_2B = "tah",ISO639_3 = "tah"},
                new LanguageCode(){ EnglishName = "Uighur",NativeName = "ئۇيغۇرچە‎, Uyghurche",ISO639_1 = "ug",ISO639_2T = "uig",ISO639_2B = "uig",ISO639_3 = "uig"},
                new LanguageCode(){ EnglishName = "Ukrainian",NativeName = "Українська",ISO639_1 = "uk",ISO639_2T = "ukr",ISO639_2B = "ukr",ISO639_3 = "ukr"},
                new LanguageCode(){ EnglishName = "Urdu",NativeName = "اردو",ISO639_1 = "ur",ISO639_2T = "urd",ISO639_2B = "urd",ISO639_3 = "urd"},
                new LanguageCode(){ EnglishName = "Uzbek",NativeName = "Oʻzbek, Ўзбек, أۇزبېك‎",ISO639_1 = "uz",ISO639_2T = "uzb",ISO639_2B = "uzb",ISO639_3 = "uzb"},
                new LanguageCode(){ EnglishName = "Venda",NativeName = "Tshivenḓa",ISO639_1 = "ve",ISO639_2T = "ven",ISO639_2B = "ven",ISO639_3 = "ven"},
                new LanguageCode(){ EnglishName = "Vietnamese",NativeName = "Tiếng Việt",ISO639_1 = "vi",ISO639_2T = "vie",ISO639_2B = "vie",ISO639_3 = "vie"},
                new LanguageCode(){ EnglishName = "Volapük",NativeName = "Volapük",ISO639_1 = "vo",ISO639_2T = "vol",ISO639_2B = "vol",ISO639_3 = "vol"},
                new LanguageCode(){ EnglishName = "Walloon",NativeName = "Walon",ISO639_1 = "wa",ISO639_2T = "wln",ISO639_2B = "wln",ISO639_3 = "wln"},
                new LanguageCode(){ EnglishName = "Welsh",NativeName = "Cymraeg",ISO639_1 = "cy",ISO639_2T = "cym",ISO639_2B = "wel",ISO639_3 = "cym"},
                new LanguageCode(){ EnglishName = "Wolof",NativeName = "Wollof",ISO639_1 = "wo",ISO639_2T = "wol",ISO639_2B = "wol",ISO639_3 = "wol"},
                new LanguageCode(){ EnglishName = "Western Frisian",NativeName = "Frysk",ISO639_1 = "fy",ISO639_2T = "fry",ISO639_2B = "fry",ISO639_3 = "fry"},
                new LanguageCode(){ EnglishName = "Xhosa",NativeName = "isiXhosa",ISO639_1 = "xh",ISO639_2T = "xho",ISO639_2B = "xho",ISO639_3 = "xho"},
                new LanguageCode(){ EnglishName = "Yiddish",NativeName = "ייִדיש",ISO639_1 = "yi",ISO639_2T = "yid",ISO639_2B = "yid",ISO639_3 = "yid"},
                new LanguageCode(){ EnglishName = "Yoruba",NativeName = "Yorùbá",ISO639_1 = "yo",ISO639_2T = "yor",ISO639_2B = "yor",ISO639_3 = "yor"},
                new LanguageCode(){ EnglishName = "Zhuang",NativeName = "Saɯ cueŋƅ, Saw cuengh",ISO639_1 = "za",ISO639_2T = "zha",ISO639_2B = "zha",ISO639_3 = "zha"},
                new LanguageCode(){ EnglishName = "Zulu",NativeName = "isiZulu",ISO639_1 = "zu",ISO639_2T = "zul",ISO639_2B = "zul",ISO639_3 = "zul"}
            }; 
        #endregion

        public static LanguageCode FindCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            var c = code.ToLower();

            if (c.Length == 2)
                return ALL_CODES.FirstOrDefault(x => x.ISO639_1 == c);
            else if (c.Length == 3)
                return ALL_CODES.FirstOrDefault(x => x.ISO639_2B == c || x.ISO639_2T == c || x.ISO639_3 == c);
            else
                return ALL_CODES.FirstOrDefault(x => x.EnglishName == c || x.NativeName == c);

        }
        public static LanguageCode DefaultCode()
        {
            return ALL_CODES.FirstOrDefault(x => x.EnglishName == "English");
        }
        
        public string EnglishName { get; set; }
        public string NativeName { get; set; }
        public string ISO639_1 { get; set; }
        public string ISO639_2T { get; set; }
        public string ISO639_2B { get; set; }
        public string ISO639_3 { get; set; }
    }
}
