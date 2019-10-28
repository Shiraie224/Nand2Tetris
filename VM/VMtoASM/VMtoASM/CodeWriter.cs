using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VMtoASM
{
    class CodeWriter
    {
        //シンボル変換用
        const string target_lcl = "local";
        const string target_arg = "argument";
        const string target_this = "this";
        const string target_that = "that";

        private StreamWriter sr;
        private bool exist_vm = false; 

        //コンストラクタ。ストリームを受け取り、書き込む準備を行う
        public CodeWriter(StreamWriter out_sr)
        {
            sr = out_sr;
        }

        //新しいVMファイルが開始されたことをお知らせ
        public void setFileName()
        {
            exist_vm = true;
        }

        //与えられた算術コマンドをアセンブリコードに変換＆出力
        public void writeArithmetic(String command)
        {
            sr.WriteLine("@SP");
            sr.WriteLine("AM=M-1");     //SPを1個下げる
            sr.WriteLine("D=M");        //D=y
            sr.WriteLine("@SP");
            sr.WriteLine("AM=M-1");     //SPを1個下げる
            sr.WriteLine("A=M");        //A=x

            if (command == "add")
            {
                sr.WriteLine("D=A+D");  //D=x+y
            }
            if (command == "sub")
            {
                sr.WriteLine("D=A-D");  //D=x-y
            }

            sr.WriteLine("@SP");        
            sr.WriteLine("A=M");        //SP位置取得
            sr.WriteLine("M=D");        //M[SP] = 算術結果
            sr.WriteLine("@SP");
            sr.WriteLine("M=M+1");      //SPを1個上げて終了
        }

        //C_PUSH or C_POPコマンドをアセンブリコードに変換＆出力
        public void writePushPop(int command, String segment, int index)
        {
            String seg = "";

            //segmentをアセンブリコード(シンボル)に
            if (segment.Contains(target_lcl))
                seg = "LCL";
            if (segment.Contains(target_arg))
                seg = "ARG";
            if (segment.Contains(target_this))
                seg = "THIS";
            if (segment.Contains(target_that))
                seg = "THAT";


            //PUSHのとき
            if (command == 1)
            {
                sr.WriteLine("@" + Convert.ToString(index));
                sr.WriteLine("D=A");        //pushする値をDに退避
                sr.WriteLine("@SP");
                sr.WriteLine("A=M");        
                sr.WriteLine("M=D");        //M[SP[SP]] = D
                sr.WriteLine("@SP");
                sr.WriteLine("M=M+1");      //SPの値をインクリ

            }

            //POPのとき
            if (command == 2)
            {
                sr.WriteLine("@" + Convert.ToString(index));
                sr.WriteLine("D=A");        //
                sr.WriteLine("@" + seg);
                sr.WriteLine("A=M");
                sr.WriteLine("D=D+A");
                sr.WriteLine("@" + seg);
                sr.WriteLine("M=D");
                sr.WriteLine("@SP");
                sr.WriteLine("M=M-1");
                sr.WriteLine("A=M");
                sr.WriteLine("D=M");
                sr.WriteLine("@" + seg);
                sr.WriteLine("A=M");
                sr.WriteLine("M=D");
                sr.WriteLine("@" + Convert.ToString(index));
                sr.WriteLine("D=A");
                sr.WriteLine("@" + seg);
                sr.WriteLine("A=M");
                sr.WriteLine("D=A-D");
                sr.WriteLine("@" + seg);
                sr.WriteLine("M=D");
            }
        }

        //出力ファイルを閉じる
        public void Close()
        {
            sr.Close();
        }
    }
}
