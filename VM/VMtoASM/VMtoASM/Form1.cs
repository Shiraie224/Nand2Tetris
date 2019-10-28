using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VMtoASM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //読み込みファイルの指定
            String filePath = @"B:\Deve\projects\07\StackArithmetic\SimpleAdd\SimpleAdd.vm";
            Encoding enc = Encoding.GetEncoding("Shift_JIS");

            //書き込み先のファイル作成
            StreamWriter writer = new StreamWriter(@"B:\Deve\projects\07\StackArithmetic\SimpleAdd\SimpleAdd.asm", false, enc);
            CodeWriter coding = new CodeWriter(writer);

            if (File.Exists(filePath))
            {

                TestBox.AppendText("ファイルオープンOK \r\n");
                StreamReader sr = new StreamReader(filePath, enc);
                Parser parse = new Parser(sr);

                int i = 1;
                int com_type;
                String arg1;
                int arg2;

                while (parse.hasMoreCommands())
                {
                    parse.advance();
                    parse.DelComment();

                    com_type = parse.commandType();
                    arg1 = parse.arg1();
                    arg2 = parse.arg2();

                    //コマンドが算術だった場合
                    if (com_type == 0)
                    {
                        coding.writeArithmetic(arg1);
                    }

                    //コマンドがプッシュかポップだった場合
                    if ((com_type == 1) || (com_type == 2))
                    {
                        coding.writePushPop(com_type, arg1, arg2);
                    }
                    
                }

                writer.Close();
                coding.Close();
                sr.Close();
            }

            //ファイルが見つからなかったとき
            else
            {
                TestBox.Text = "そんなファイルはねえ！";
            }
        }
    }
}
