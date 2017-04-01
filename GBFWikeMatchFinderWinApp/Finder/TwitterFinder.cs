using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace GBFWikeMatchFinderWinApp.Finder
{
    public class TwitterFinder : IMultiBattleFinder
    {
        private IFilteredStream _twitterStream;

        public event Action<string, string> OnBattleFound;
        public event Action<string> OnWriteLog;

        public void Execute(List<MultiBattleDefine> selectedBattles)
        {
            WriteLog(" start!");
            Task.Run((() => ExecuteTwitterFinder(selectedBattles)));
        }

        private void ExecuteTwitterFinder(List<MultiBattleDefine> selectedBattles)
        {
            ExceptionHandler.SwallowWebExceptions = false;
            try
            {
                string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
                string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
                string accessToken = ConfigurationManager.AppSettings["AccessToken"];
                string accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];

                Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                
                TweetinviEvents.QueryBeforeExecute += (sender, args) =>
                {
                    //Console.WriteLine(args.QueryURL);
                };

                _twitterStream = Stream.CreateFilteredStream();

                //加入等級Filter
                selectedBattles.Select(s => s.BattleLevel).Distinct().ForEach(e =>
                {
                    _twitterStream.AddTrack("Lv" + e);
                });
                _twitterStream.AddTweetLanguageFilter(LanguageFilter.Japanese);


                //組合name的pattern
                string namePatternString = $"(?<name>{string.Join("|", selectedBattles.Select(s => s.Value))})";

                Regex pattern = new Regex(@"参戦ID：(?<matchid>([a-zA-Z0-9]){8})\s*" + namePatternString);

                _twitterStream.MatchingTweetReceived += (sender, args) =>
                {
                    var tweet = args.Tweet;
                    var match = pattern.Match(tweet.FullText);
                    if (match.Success)
                    {
                        OnBattleFound?.Invoke(match.Groups["matchid"].ToString(), match.Groups["name"].ToString());
                    }
                    else
                    {
                        WriteLog("Not matched");
                    }
                };

                _twitterStream.StartStreamMatchingAnyCondition();
            }
            catch (TwitterException ex)
            {
                if (null != _twitterStream)
                {
                    _twitterStream.StopStream();
                }
                WriteLog(ex.ToString());
            }
        }

        //public void Pause()
        //{
        //    _twitterStream.PauseStream();
        //    OnWriteLog?.Invoke($"{nameof(TwitterFinder)} paused!");
        //}

        public void Stop()
        {
            _twitterStream.StopStream();
            WriteLog(" stopped!");
        }

        private void WriteLog(string msg)
        {
            OnWriteLog?.Invoke($"{nameof(TwitterFinder)} {msg}");
        }
    }
}
