using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{    
    class Parser
    {
        const int A_COMMAND = 0;
        const int C_COMMAND = 1;
        const int L_COMMAND = 2;
        const int N_COMMAND = 9;
        const string target_A = "@";
        const string target_L = "(";
        const string target_C1 = "=";
        const string target_C2 = ";";

        private StreamReader sr;
        private String cr_com;

        //コンストラクタ
        public Parser(StreamReader in_sr)
        {
            sr = in_sr;
        }

        //次の行が存在するか？ 存在する場合、True
        public bool hasMoreCommands()
        {
            if (sr.Peek() >= 0)
            {
                return true;
            }
            else
                return false;
        }

        //1行読み込み
        public void advance()
        {
            cr_com = sr.ReadLine();
        }

        //コメント部分を削り取り
        public void DelComment()
        {
            int loc_comment = cr_com.IndexOf("//");
            //1行の中に"//"が含まれていれば、"//"以降を全て取り除く
            if (loc_comment >= 0)
            {
                cr_com = cr_com.Remove(loc_comment, cr_com.Length - loc_comment);  
            }
            cr_com = cr_com.Replace(" ", "");
        }

        //現コマンドの種類を返す
        public int commandType()
        {
            if (cr_com.Contains(target_A))
            {
                return A_COMMAND;
            }
            else if (cr_com.Contains(target_L))
            {
                return L_COMMAND;
            }
            else if(cr_com.Contains(target_C1)|| cr_com.Contains(target_C2)) 
            {
                return C_COMMAND;
            }
            return N_COMMAND;
        }

        //現コマンドがA/Lコマンドの場合、中身の文字列のみ返す
        public string symbol()
        {
            String symbol = cr_com;

            //"@"を消す
            int loc_at = symbol.IndexOf("@");
            if (loc_at >= 0)
            {
                symbol = symbol.Remove(loc_at, 1);
                
            }

            //"("を消す
            int loc_l = symbol.IndexOf("(");
            if (loc_l >= 0)
            {
                symbol = symbol.Remove(loc_l, 1);
                
            }

            //")"を消す
            int loc_r = symbol.IndexOf(")");
            if (loc_r >= 0)
            {
                symbol = symbol.Remove(loc_r, 1);
                
            }

            return symbol;
        }

        //2回目の湯通し用。@の時だけ中身を返す。()付のラベルは無視するため
        public string symbol2()
        {
            String symbol = cr_com;

            //"@"を消す
            int loc_at = symbol.IndexOf("@");
            if (loc_at >= 0)
            {
                symbol = symbol.Remove(loc_at, 1);

            }
            return symbol;
        }

        //C命令のdestニーモニックを返す
        public string dest()
        {
            String n_dest = "000";
            String dest = cr_com;

            int loc_eq = dest.IndexOf("=");
            if (loc_eq >= 0)
            {
                dest = dest.Remove(loc_eq, dest.Length-loc_eq);
            }
            int loc_sm = dest.IndexOf(";");
            if (loc_sm >= 0)
            {
                dest = "";
            }

            switch (dest)
            {
                case "M":
                    n_dest = "001";
                    return n_dest;
                case "D":
                    n_dest = "010";
                    return n_dest;
                case "MD":
                    n_dest = "011";
                    return n_dest;
                case "A":
                    n_dest = "100";
                    return n_dest;
                case "AM":
                    n_dest = "101";
                    return n_dest;
                case "AD":
                    n_dest = "110";
                    return n_dest;
                case "AMD":
                    n_dest = "111";
                    return n_dest;
            }
            return n_dest;
        }

        //C命令のcompニーモニックを返す
        public string comp()
        {
            String n_comp = "0000000";
            String comp = cr_com;           

            int loc_eq = comp.IndexOf("=");
            if (loc_eq >= 0)
            {
                comp = comp.Remove(0, loc_eq+1);                
            }

            int loc_sm = comp.IndexOf(";");
            if (loc_sm >= 0)
            {
                comp = comp.Remove(loc_sm, comp.Length-loc_sm);
            }

            switch (comp)
            {
                case "0":
                    n_comp = "0101010";
                    return n_comp;
                case "1":
                    n_comp = "0111111";
                    return n_comp;
                case "-1":
                    n_comp = "0111010";
                    return n_comp;
                case "D":
                    n_comp = "0001100";
                    return n_comp;
                case "A":
                    n_comp = "0110000";
                    return n_comp;
                case "!D":
                    n_comp = "0001101";
                    return n_comp;
                case "!A":
                    n_comp = "0110001";
                    return n_comp;
                case "-D":
                    n_comp = "0001111";
                    return n_comp;
                case "-A":
                    n_comp = "0110011";
                    return n_comp;
                case "D+1":
                    n_comp = "0011111";
                    return n_comp;
                case "A+1":
                    n_comp = "0110111";
                    return n_comp;
                case "D-1":
                    n_comp = "0001110";
                    return n_comp;
                case "A-1":
                    n_comp = "0110010";
                    return n_comp;
                case "D+A":
                    n_comp = "0000010";
                    return n_comp;
                case "D-A":
                    n_comp = "0010011";
                    return n_comp;
                case "A-D":
                    n_comp = "0000111";
                    return n_comp;
                case "D&A":
                    n_comp = "0000000";
                    return n_comp;
                case "D|A":
                    n_comp = "0010101";
                    return n_comp;
                case "M":
                    n_comp = "1110000";
                    return n_comp;
                case "!M":
                    n_comp = "1110001";
                    return n_comp;
                case "-M":
                    n_comp = "1110011";
                    return n_comp;
                case "M+1":
                    n_comp = "1110111";
                    return n_comp;
                case "M-1":
                    n_comp = "1110010";
                    return n_comp;
                case "D+M":
                    n_comp = "1000010";
                    return n_comp;
                case "D-M":
                    n_comp = "1010011";
                    return n_comp;
                case "M-D":
                    n_comp = "1000111";
                    return n_comp;
                case "D&M":
                    n_comp = "1000000";
                    return n_comp;
                case "D|M":
                    n_comp = "1010101";
                    return n_comp;
            }
            return n_comp;
        }

        //C命令のjumpニーモニックを返す
        public string jump()
        {
            String n_jump = "000";
            String jump = cr_com;

            int loc_sm = jump.IndexOf(";");
            if (loc_sm >= 0)
            {
                jump = jump.Remove(0, loc_sm+1);

                switch (jump)
                {
                    case "JGT":
                        n_jump = "001";
                        return n_jump;
                    case "JEQ":
                        n_jump = "010";
                        return n_jump;
                    case "JGE":
                        n_jump = "011";
                        return n_jump;
                    case "JLT":
                        n_jump = "100";
                        return n_jump;
                    case "JNE":
                        n_jump = "101";
                        return n_jump;
                    case "JLE":
                        n_jump = "110";
                        return n_jump;
                    case "JMP":
                        n_jump = "111";
                        return n_jump;
                }
            }
            return n_jump;
        }
    }
}
