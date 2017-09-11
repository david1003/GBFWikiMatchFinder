using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GBFFinderLibrary;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace GBFTwitterFinderWeb.Finder
{
    public class TwitterFinder : IMultiBattleFinder
    {
        private IFilteredStream _twitterStream;

        public event Action<string, string, string> OnBattleFound;
        public event Action<string> OnWriteLog;

        public void Execute(List<MultiBattleDefine> selectedBattles)
        {
            WriteLog(" start!");
            Task.Run((() => ExecuteTwitterFinder(selectedBattles)));
        }

        private void ExecuteTwitterFinder(List<MultiBattleDefine> selectedBattles)
        {
            try
            {
                ExceptionHandler.SwallowWebExceptions = false;

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
                selectedBattles.Select(s => s.Level).Distinct().ForEach(e =>
                {
                    _twitterStream.AddTrack("Lv" + e);
                });
                _twitterStream.AddTweetLanguageFilter(LanguageFilter.Japanese);

                //加入名字Filter 降低通知頻率(中間名字有符號的會捉不到，放棄)
                //selectedBattles.ForEach(e =>
                //{
                //    _twitterStream.AddTrack("Lv" + e.Level + " " + e.Value);
                //});

                //組合name的pattern 
                //string namePatternString = $"(?<name>{string.Join("|", selectedBattles.Select(s => $"Lv{s.Level}\\s*{s.Value}"))})";
                //Regex pattern = new Regex(@"参戦ID：(?<matchid>([a-zA-Z0-9]){8})\s*" + namePatternString);
                

                _twitterStream.MatchingTweetReceived += (sender, args) =>
                {
                    var tweet = args.Tweet;
                    OnBattleFound?.Invoke(string.Empty, string.Empty, tweet.FullText);
                };
                _twitterStream.StartStreamMatchingAnyCondition();

                //var SStatus = _twitterStream.StreamState;

                if (_twitterStream.StreamState == StreamState.Stop)
                {
                    WriteLog(" stopped!");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                if (null != _twitterStream && _twitterStream.StreamState != StreamState.Stop)
                {
                    _twitterStream.StopStream();
                    WriteLog(" Auto stopped!");
                }
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
            WriteLog(" stopping!");
        }

        private void WriteLog(string msg)
        {
            OnWriteLog?.Invoke($"{nameof(TwitterFinder)} {msg}");
        }
    }
}
