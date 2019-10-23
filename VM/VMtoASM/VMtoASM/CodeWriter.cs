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

        private StreamReader sr;

        //出力ファイル/ストリームを開いて、書き込む準備をする
        public CodeWriter(StreamReader out_sr)
        {
            sr = out_sr;
        }

        //CodeWriterモジュールに新しいVMファイルの変換を開始したことをお知らせ
        public void setFileName(String fileName)
        {

        }

        //与えられた算術コマンドをアセンブリコードに変換して書き込む
        public void writeArithmetic(String command)
        {

        }

        //C_PUSHまたはC_POPをアセンブリコードに変換して書き込む
        public void writePushPop(String command, String segment, int index)
        {

        }

        //出力ファイルを閉じる
        public void close()
        {
            sr.Close();
        }

    }
}
