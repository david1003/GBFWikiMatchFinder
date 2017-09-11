using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GBFFinderLibrary;

namespace GBFTwitterFinderWeb.Models
{
    public static  class GlobalStorage
    {
        static GlobalStorage()
        {
            BossDatas = new List<MultiBattleDefine>();
            BossDatas.Add(new MultiBattleDefine("ティアマト・マグナ", @"ティアマト", 50));
            BossDatas.Add(new MultiBattleDefine("ユグドラシル・マグナ", @"ユグドラシル", 60));
            BossDatas.Add(new MultiBattleDefine("リヴァイアサン・マグナ", @"リヴァイアサン", 60));
            BossDatas.Add(new MultiBattleDefine("コロッサス・マグナ", @"コロッサス", 70));
            BossDatas.Add(new MultiBattleDefine("シュヴァリエ", @"シュヴァリエ", 75));
            BossDatas.Add(new MultiBattleDefine("セレスト", @"セレスト", 75));
            
            BossDatas.Add(new MultiBattleDefine("アグニス", @"アグニス", 90));
            BossDatas.Add(new MultiBattleDefine("ゼピュロス", @"ゼピュロス", 90));
            BossDatas.Add(new MultiBattleDefine("ティターン", @"ティターン", 90));
            BossDatas.Add(new MultiBattleDefine("ネプチューン", @"ネプチューン", 90));

            BossDatas.Add(new MultiBattleDefine("Dエンジェル・オリヴィエ", @"Dエンジェル", 100));
            BossDatas.Add(new MultiBattleDefine("アテナ", @"アテナ", 100));
            BossDatas.Add(new MultiBattleDefine("アポロン", @"アポロン", 100));
            BossDatas.Add(new MultiBattleDefine("ウリエル", @"ウリエル", 100));
            BossDatas.Add(new MultiBattleDefine("オーディン", @"オーディン", 100));
            BossDatas.Add(new MultiBattleDefine("ガルーダ", @"ガルーダ", 100));
            BossDatas.Add(new MultiBattleDefine("グラニ", @"グラニ", 100));
            BossDatas.Add(new MultiBattleDefine("ガルーダ", @"ガブリエル", 100));
            BossDatas.Add(new MultiBattleDefine("ナタク", @"ナタク", 100));
            BossDatas.Add(new MultiBattleDefine("バアル", @"バアル", 100));
            BossDatas.Add(new MultiBattleDefine("フラム＝グラス", @"フラム", 100));
            BossDatas.Add(new MultiBattleDefine("マキュラ・マリウス", @"マキュラ", 100));
            BossDatas.Add(new MultiBattleDefine("メドゥーサ", @"メドゥーサ", 100));
            BossDatas.Add(new MultiBattleDefine("リッチ", @"リッチ", 100));

            BossDatas.Add(new MultiBattleDefine("コロッサス・マグナ", @"コロッサス", 100));
            BossDatas.Add(new MultiBattleDefine("シュヴァリエ・マグナ", @"シュヴァリエ", 100));
            BossDatas.Add(new MultiBattleDefine("セレスト・マグナ", @"セレスト", 100));
            BossDatas.Add(new MultiBattleDefine("ティアマト・マグナ＝エア", @"ティアマト", 100));
            BossDatas.Add(new MultiBattleDefine("ユグドラシル・マグナ", @"ユグドラシル", 100));
            BossDatas.Add(new MultiBattleDefine("リヴァイアサン・マグナ", @"リヴァイアサン", 100));
            BossDatas.Add(new MultiBattleDefine("リヴァイアサン・マグナ", @"リヴァイアサン", 100));
            BossDatas.Add(new MultiBattleDefine("リヴァイアサン・マグナ", @"リヴァイアサン", 100));

            BossDatas.Add(new MultiBattleDefine("ミカエル", @"ミカエル", 100));
            BossDatas.Add(new MultiBattleDefine("ガブリエル", @"ガブリエル", 100));
            BossDatas.Add(new MultiBattleDefine("ウリエル", @"ウリエル", 100));
            BossDatas.Add(new MultiBattleDefine("ラファエル", @"ラファエル", 100));
            BossDatas.Add(new MultiBattleDefine("プロトバハムート", @"プロトバハムート", 100));
            BossDatas.Add(new MultiBattleDefine("グランデ", @"ジ・オーダー・グランデ", 100));
            BossDatas.Add(new MultiBattleDefine("黄龍", @"黄龍", 100));
            BossDatas.Add(new MultiBattleDefine("黒麒麟", @"黒麒麟", 100));

            BossDatas.Add(new MultiBattleDefine("ローズクイーン", @"ローズクイーン", 110));
            BossDatas.Add(new MultiBattleDefine("プロトバハムート", @"プロトバハムート", 150));
        }


        public static List<MultiBattleDefine> BossDatas;
    }
}