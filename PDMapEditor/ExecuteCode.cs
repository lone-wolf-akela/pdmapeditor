﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace PDMapEditor
{
    public partial class ExecuteCode : Form
    {
        private static string code = string.Empty;
        private static string errors = string.Empty;

        public ExecuteCode()
        {
            InitializeComponent();
        }

        public void Open()
        {
            boxCode.Margins[0].Width = 24;

            boxCode.StyleResetDefault();
            boxCode.Styles[Style.Default].Font = "Courier New";
            boxCode.Styles[Style.Default].Size = 10;
            boxCode.StyleClearAll();

            boxCode.Lexer = ScintillaNET.Lexer.Lua;
            boxCode.Styles[Style.Lua.Default].ForeColor = Color.Black;
            boxCode.Styles[Style.Lua.Comment].ForeColor = Color.FromArgb(255, 0, 128, 0);
            boxCode.Styles[Style.Lua.CommentLine].ForeColor = Color.FromArgb(255, 0, 128, 0);
            boxCode.Styles[Style.Lua.CommentDoc].ForeColor = Color.FromArgb(255, 0, 128, 128);
            boxCode.Styles[Style.Lua.LiteralString].ForeColor = Color.FromArgb(255, 149, 0, 74);
            boxCode.Styles[Style.Lua.Preprocessor].ForeColor = Color.FromArgb(255, 128, 64, 0);
            boxCode.Styles[Style.Lua.Number].ForeColor = Color.FromArgb(255, 255, 128, 0);
            boxCode.Styles[Style.Lua.String].ForeColor = Color.FromArgb(255, 128, 128, 128);
            boxCode.Styles[Style.Lua.Character].ForeColor = Color.FromArgb(255, 128, 128, 128);
            boxCode.Styles[Style.Lua.Operator].ForeColor = Color.FromArgb(255, 0, 0, 128);

            boxCode.SetKeywords(0, "and break do else elseif end false for function goto if in local nil not or repeat return then true until while");
            boxCode.Styles[Style.Lua.Word].ForeColor = Color.FromArgb(255, 0, 0, 255);

            boxCode.SetKeywords(1, "_ENV _G _VERSION assert collectgarbage dofile error getfenv getmetatable ipairs load loadfile loadstring module next pairs pcall print rawequal rawget rawlen rawset require select setfenv setmetatable tonumber tostring type unpack xpcall string table math bit32 coroutine io os debug package __index __newindex __call __add __sub __mul __div __mod __pow __unm __concat __len __eq __lt __le __gc __mode");
            boxCode.Styles[Style.Lua.Word2].ForeColor = Color.FromArgb(255, 0, 128, 192);

            boxCode.SetKeywords(2, "byte char dump find format gmatch gsub len lower match rep reverse sub upper abs acos asin atan atan2 ceil cos cosh deg exp floor fmod frexp ldexp log log10 max min modf pow rad random randomseed sin sinh sqrt tan tanh arshift band bnot bor btest bxor extract lrotate lshift replace rrotate rshift shift string.byte string.char string.dump string.find string.format string.gmatch string.gsub string.len string.lower string.match string.rep string.reverse string.sub string.upper table.concat table.insert table.maxn table.pack table.remove table.sort table.unpack math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.cosh math.deg math.exp math.floor math.fmod math.frexp math.huge math.ldexp math.log math.log10 math.max math.min math.modf math.pi math.pow math.rad math.random math.randomseed math.sin math.sinh math.sqrt math.tan math.tanh bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.lrotate bit32.lshift bit32.replace bit32.rrotate bit32.rshift");
            boxCode.Styles[Style.Lua.Word3].ForeColor = Color.FromArgb(255, 128, 0, 255);

            boxCode.SetKeywords(3, "close flush lines read seek setvbuf write clock date difftime execute exit getenv remove rename setlocale time tmpname coroutine.create coroutine.resume coroutine.running coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.popen io.read io.tmpfile io.type io.write io.stderr io.stdin io.stdout os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname debug.debug debug.getfenv debug.gethook debug.getinfo debug.getlocal debug.getmetatable debug.getregistry debug.getupvalue debug.getuservalue debug.setfenv debug.sethook debug.setlocal debug.setmetatable debug.setupvalue debug.setuservalue debug.traceback debug.upvalueid debug.upvaluejoin package.cpath package.loaded package.loaders package.loadlib package.path package.preload package.seeall");
            boxCode.Styles[Style.Lua.Word4].ForeColor = Color.FromArgb(255, 0, 0, 160);

            boxCode.Text = code;
            boxErrors.Text = errors;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            boxErrors.Clear();

            string[] errors = LuaMap.ExecuteCode(boxCode.Text);

            foreach (string error in errors)
                boxErrors.AppendText(error + "\n");
        }

        private void ExecuteCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            code = boxCode.Text;
            errors = boxErrors.Text;
        }
    }
}
