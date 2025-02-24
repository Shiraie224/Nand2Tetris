﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VMtoASM
{
    class Parser
    {
        const int C_ARITHMETIC = 0;
        const int C_PUSH = 1;
        const int C_POP = 2;
        const int C_FUNCTION = 3;
        const int C_CALL = 4;
        const int C_LABEL = 5;
        const int C_GOTO = 6;
        const int C_IF = 7;       
        const int C_RETURN = 8;        
        const int C_NONE = 15;
        const string target_ARITH1 = "add";
        const string target_ARITH2 = "sub";
        const string target_PUSH = "push";
        const string target_POP = "pop";
        const string target_LABEL = "hoge";
        const string target_GOTO = "hoeg";
        const string target_IF = "if";
        const string target_FUNCTION = "hoge";
        const string target_RETURN = "return";
        const string target_CALL = "call";


        private StreamReader sr;
        private String cr_com　="";
        private String[] arg;

        //入力ファイル/ストリームを開いて、パースを行う準備をする
        public Parser(StreamReader in_sr)
        {
            sr = in_sr;
        }

        //さらにコマンドが存在するか？
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
        }

        //現コマンドの種類を返す
        public int commandType()
        {
            if (cr_com.Contains(target_ARITH1) || cr_com.Contains(target_ARITH2))
            {
                return C_ARITHMETIC;
            }
            if (cr_com.Contains(target_PUSH))
            {
                return C_PUSH;
            }
            if (cr_com.Contains(target_POP))
            {
                return C_POP;
            }

            return C_NONE;
        }

        //現コマンドの最初の引数を返す
        public string arg1()
        {
            string arg1 = "";
            
            //算術演算子の場合
            if(commandType() == 0)
            {
                if (cr_com.Contains(target_ARITH1))
                {
                    arg1 = "add";
                    return arg1;
                }
                if (cr_com.Contains(target_ARITH2))
                {
                    arg1 = "sub";
                    return arg1;
                }
            }

            if ((commandType() >= 1) & commandType() <= 8)
            {
                arg = cr_com.Split(' ');
                return arg[1];
            }

            return arg1;
        }

        //現コマンドの2番目の引数を返す
        public int arg2()
        {
            int arg2 = 0;

            if ((commandType() >= 1) & commandType() <= 4)
            {
                arg = cr_com.Split(' ');
                arg2 = Convert.ToInt32(arg[2]);
                return arg2;
            }

            return arg2;
        }

    }
}
