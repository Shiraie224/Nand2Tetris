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

namespace Assembler
{
    public partial class Form1 : Form
    {
        //アドレスの長さ
        const int add_length = 16;

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            String filePath = @"C:\Study\NAND2Tetris\nand2tetris\projects\06\add\add.asm";

            if (File.Exists(filePath))
            {
                TestBox.AppendText("ファイルオープンOK \r\n");
                Encoding enc = Encoding.GetEncoding("Shift_JIS");
                StreamReader sr = new StreamReader(filePath, enc);
                StreamReader sr2 = new StreamReader(filePath, enc);
                Parser parse = new Parser(sr);
                Parser parse2 = new Parser(sr2);
                SymbolTable sym_table = new SymbolTable();

                //デバッグ用の変数。yudoshi:湯通し回数。
                int yudoshi = 1;
                int asm_row = 1;


                //1回目の湯通し。シンボルテーブルをつくるだけ
                int add_base = 0;
                TestBox.AppendText("1回目パース(シンボルテーブル作成) \r\n");
                while (parse.hasMoreCommands())
                {
                    TestBox.AppendText(yudoshi.ToString() +  "回目パース,"+ asm_row.ToString() + "行目解読中 \r\n");
                    parse.advance();
                    parse.DelComment();
                    int com_type = parse.commandType();
                    if(com_type == 0 || com_type == 2)
                    {
                        String nakami = parse.symbol();

                        int i = 0;
                        bool result = int.TryParse(nakami, out i);

                        if (result)
                        {
                            //ただの数値。2回目の湯通しでパースする
                        }
                        else
                        {
                            if (sym_table.contains(nakami))
                            {
                                //2回目の湯通しでパースする
                            }
                            else
                            {
                                TestBox.AppendText(nakami + " をテーブルに追加。アドレスは、" + add_base.ToString() + " \r\n");
                                sym_table.addEntry(nakami, add_base);
                                add_base = add_base + 1;
                            }
                        }
                    }
                    asm_row++;
                }

                //書込み先のファイル作成
                StreamWriter writer = new StreamWriter(@"C:\Study\NAND2Tetris\nand2tetris\projects\06\add\add.hack", false, enc);

                //2回目の湯通し。バイナリデータ化
                String mnemo;
                String dest;
                String comp;
                String jump;
                yudoshi++;
                asm_row = 1;

                while (parse2.hasMoreCommands())
                {
                    TestBox.AppendText(yudoshi.ToString() + "回目パース," + asm_row.ToString() + "行目解読中 \r\n");

                    mnemo = "";
                    parse2.advance();
                    parse2.DelComment();
                    int com_type = parse2.commandType();
                    if (com_type == 0 || com_type == 2)
                    {
                        String nakami = parse2.symbol2();

                        int i = 0;
                        bool result = int.TryParse(nakami, out i);

                        if (result)
                        {
                            TestBox.AppendText(nakami + " はただの数値なので、2進数に変更する \r\n");
                            int val_nakami = Convert.ToInt32(nakami);
                            mnemo = Convert.ToString(val_nakami, 2);

                            //アドレスの長さに合わせる
                            while (mnemo.Length < add_length)
                            {
                                mnemo = "0" + mnemo;                                
                            }
                            writer.WriteLine(mnemo);
                        }
                        else
                        {
                            if (sym_table.contains(nakami))
                            {
                                TestBox.AppendText(nakami + " がシンボルテーブルに存在するため、アドレス取得 \r\n");
                                mnemo = sym_table.getAddress(nakami);
                                writer.WriteLine(mnemo);
                            }
                            else
                            {
                                //1回目の湯通しでシンボルテーブル作成済。何もしない

                                // 10/17の備忘録：(hogehoge)は、上もスルーしないといけない!

                                TestBox.AppendText("スルー湯通し \r\n");
                            }
                        }                        
                    }
                    else if (com_type == 1)
                    {
                        dest = parse2.dest();
                        comp = parse2.comp();
                        jump = parse2.jump();

                        TestBox.AppendText("Comp : " + comp + ", Dest : "+ dest + ", Jump : "+ jump+ " だよ \r\n");

                        mnemo = "111" + comp + dest + jump;
                        writer.WriteLine(mnemo);
                    }
                    asm_row++;
                }

                sr.Close();
                sr2.Close();
               
                TestBox.AppendText("完了");

                writer.Close();

            }
            else
            {
                TestBox.Text = "そんなファイルはねえ！";
            }
        }
    }
}
