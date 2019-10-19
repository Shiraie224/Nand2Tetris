using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class SymbolTable
    {
        DataSet dataSet = new DataSet();
        DataTable Stable = new DataTable("Stable");

        //変数シンボルの開始アドレス
        const int var_symbol = 16;
        //ROMアドレスの長さ
        const int add_length = 16;

        public SymbolTable()
        {
            //テーブル作成
            Stable.Columns.Add("symbol");
            Stable.Columns.Add("adress");

            dataSet.Tables.Add(Stable);

            //初期化
            Stable.Rows.Add("SP", "0000000000000000");
            Stable.Rows.Add("LCL", "0000000000000001");
            Stable.Rows.Add("ARG", "0000000000000010");
            Stable.Rows.Add("THIS", "0000000000000011");
            Stable.Rows.Add("THAT", "0000000000000100");
            Stable.Rows.Add("SCREEN", "0100000000000000");
            Stable.Rows.Add("KBD", "0110000000000000");
            Stable.Rows.Add("R0", "0000000000000000");
            Stable.Rows.Add("R1", "0000000000000001");
            Stable.Rows.Add("R2", "0000000000000010");
            Stable.Rows.Add("R3", "0000000000000011");
            Stable.Rows.Add("R4", "0000000000000100");
            Stable.Rows.Add("R5", "0000000000000101");
            Stable.Rows.Add("R6", "0000000000000110");
            Stable.Rows.Add("R7", "0000000000000111");
            Stable.Rows.Add("R8", "0000000000001000");
            Stable.Rows.Add("R9", "0000000000001001");
            Stable.Rows.Add("R10", "0000000000001010");
            Stable.Rows.Add("R11", "0000000000001011");
            Stable.Rows.Add("R12", "0000000000001100");
            Stable.Rows.Add("R13", "0000000000001101");
            Stable.Rows.Add("R14", "0000000000001110");
            Stable.Rows.Add("R15", "0000000000001111");            
        }

        //テーブルに(symbol,address)のペアを追加する
        public void addEntry(String symbol, int address)
        {
            address = address + var_symbol;
            String b_add = Convert.ToString(address, 2);

            //アドレスの長さに合わせる
            while (b_add.Length < add_length)
            {
                b_add = "0" + b_add;
            }

            Stable.Rows.Add(symbol, b_add);
        }

        //symbolがテーブルに存在するか？
        public bool contains(String symbol)
        {
            DataRow[] rows = Stable.Select("symbol = '" + symbol + "'");
            if (rows.Length == 0)
            {
                return false;
            }
            return true;
        }

        //symbolに結びつけられたアドレスを返す
        public string getAddress(String symbol)
        {            
            DataRow[] rows = Stable.Select("symbol = '" + symbol + "'");
            String address = Convert.ToString(rows[0][1]);

            return address;
        }
    }
}
