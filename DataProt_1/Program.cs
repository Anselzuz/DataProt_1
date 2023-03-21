Dictionary<char, int> GenerateDictionary(string alphabet)
{
    Dictionary<char, int> dic = new();
    for (int i = 0; i < alphabet.Length; ++i)
        dic.Add(alphabet[i], i+1);
    return dic;
}

int[] GenerateIdKey(Dictionary<char, int> dic, string key)
{
    int[] idKey = new int[key.Length];
    for (int i = 0; i < key.Length; ++i)
    {
        int id;
        dic.TryGetValue(key[i], out id);
        idKey[i] = id;
    }
    return idKey;
}

List<int> HighLetters(string txt)
{
    string lowerTxt = txt.ToLower();
    List<int> list = new List<int>();
    for (int i = 0; i < txt.Length; ++i)
    {
        if (lowerTxt[i] != txt[i])
            list.Add(i);
    }
    return list;
}

string Encoding(string txt, string key, string alphabet)
{
    Dictionary<char, int> dic = GenerateDictionary(alphabet);
    int[] idKey = GenerateIdKey(dic, key);
    string result = "";
    List<int> high = HighLetters(txt);
    txt = txt.ToLower();

    int keyNum = 0;
    for (int i = 0; i < txt.Length; ++i)
    {
        int idElTxt, idNewEl;
        dic.TryGetValue(txt[i], out idElTxt);
        if (idElTxt == 0 || idKey[keyNum] == 0)
        {
            if (high.FindIndex(el => el == i) == -1)
                result += txt[i];
            else
                result += txt[i].ToString().ToUpper();
        }
        else
        {
            idNewEl = --idElTxt + idKey[keyNum++];
            if (keyNum == idKey.Length)
                keyNum = 0;
            if (idNewEl > alphabet.Length)
                idNewEl -= alphabet.Length;
            if (high.FindIndex(el => el == i) == -1)
                result += alphabet[--idNewEl];
            else
                result += alphabet[--idNewEl].ToString().ToUpper();
        }
    }

    return result;
}

string Decoding(string encodeTxt, string key, string alphabet)
{
    Dictionary<char, int> dic = GenerateDictionary(alphabet);
    int[] idKey = GenerateIdKey(dic, key);
    string result = "";
    List<int> high = HighLetters(encodeTxt);
    encodeTxt = encodeTxt.ToLower();

    int keyNum = 0;
    for (int i = 0; i < encodeTxt.Length; ++i)
    {
        int idElTxt, idNewEl;
        dic.TryGetValue(encodeTxt[i], out idElTxt);
        if (idElTxt == 0 || idKey[keyNum] == 0)
        {
            if (high.FindIndex(el => el == i) == -1)
                result += encodeTxt[i];
            else
                result += encodeTxt[i].ToString().ToUpper();
        }
        else
        {
            idNewEl = ++idElTxt - idKey[keyNum++];
            if (keyNum == idKey.Length)
                keyNum = 0;
            if (idNewEl <= 0)
                idNewEl += alphabet.Length;
            if (high.FindIndex(el => el == i) == -1)
                result += alphabet[--idNewEl];
            else
                result += alphabet[--idNewEl].ToString().ToUpper();
        }
    }

    return result;
}

string txt = "От улыбки каждый день светлей.";
string key = "мышь";
const string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
//_.,!?;:\"'

Console.WriteLine("Исходный текст:\t\t" + txt);
string encodeTxt = Encoding(txt, key, alphabet);
Console.WriteLine("Закодированный текст:\t" + encodeTxt);
string decodeText = Decoding(encodeTxt, key, alphabet);
Console.WriteLine("Расшифрованный текст:\t" + decodeText);