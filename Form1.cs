using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RU_Morse_CodeDecode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<char> RU_Letters = new List<char> { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

        public List<string> Morse_Letters = new List<string> { ".-", "-...", ".--", "--.", "-..", ".", ".", "...-", "--..", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", ".-.", "...", "-", "..-", "..-.", "....", "-.-.", "---.", "----", "--.-", "--.--", "-.--", "-..-", "..-..", "..--", ".-.-", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----.", "......" };


        #region WordsToMorse
        public string WordsToMorse(string Words_input)
        {
            string result = string.Empty;
            Words_input = Words_input.ToUpper();

            for (int i = 0; i < Words_input.Length; i++)
            { 
                if (RU_Letters.Contains(Words_input[i]))
                {
                    int index = RU_Letters.IndexOf(Words_input[i]);
                    result +=Morse_Letters[index] + " ";
                }
                else if (Words_input[i] == ' ')
                {result += "   ";}
            
            }
                return result.Trim();
        }
        #endregion

        #region MorseToWords
        public string MorseToWords(string Morse_input)
        {
            string result = string.Empty;


                Morse_input = Morse_input.Replace("   ", "&");

                List<string> Separated = new List<string>();
                Separated = Morse_input.Split('&').ToList();
                List<List<string>> new_separated = new List<List<string>>();

                for (int n = 0; n < Separated.Count; n++)
                {
                    new_separated.Add(new List<string>(Separated[n].Split(' ').ToList()));
                }
                    for (int p = 0; p < Separated.Count; p++)
                    {
                        for (int j = 0; j < new_separated[p].Count; j++)
                        {
                            if (Morse_Letters.Contains(new_separated[p][j]))
                            {
                                int index = Morse_Letters.IndexOf(new_separated[p][j]);
                                result += RU_Letters[index];
                            }
                        }
                       result +=" ";
                    }
            return result.Trim();
        }
        #endregion

        #region Tests
        private bool Tests()
        {
            bool Test1, Test2, Test3, result;
            if (WordsToMorse("Это тестовый пример").Equals("..-.. - ---    - . ... - --- .-- -.-- .---    .--. .-. .. -- . .-.")
                && MorseToWords("..-.. - ---    - . ... - --- .-- -.-- .---    .--. .-. .. -- . .-.").Equals("ЭТО ТЕСТОВЫЙ ПРИМЕР"))
                Test1 = true;
            else Test1 = false;
            if (WordsToMorse("0123456789").Equals("----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----.")
                && MorseToWords("----- .---- ..--- ...-- ....- ..... -.... --... ---.. ----.").Equals("0123456789"))
                Test2 = true;
            else Test2 = false;
            if (WordsToMorse("!№;%:?*() 0").Equals("-----")
                && MorseToWords("-----").Equals("0"))
                Test3 = true;
            else Test3 = false;

            return result = Test1 && Test2 && Test3;
        }
        #endregion


        private void button_code_Click(object sender, EventArgs e)
        {
            if (Tests()) //выполнение будет только в случае успешных тестов, тесты быстрые, сильно не замедлят работу в таком случае
            richTextBox_morse.Text = WordsToMorse(richTextBox_words.Text);
        }

        private void button_decode_Click(object sender, EventArgs e)
        {
            if (Tests()) //выполнение будет только в случае успешных тестов
            richTextBox_words.Text = MorseToWords(richTextBox_morse.Text);
        }
    }
}
