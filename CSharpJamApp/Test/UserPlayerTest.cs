
using CSharpJamApp.Models;
using Xunit;

namespace CSharpJamApp.Test
{
    public class UserPlayerTest
    {

        //[Fact]
        //public void WeakPlayer()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);           

        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void StrongPlayer()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);          


        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void ModeratePlayer()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Moderate);


        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void ModeratePlayerfDamagePercentage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Moderate);


        //    double actual = userPlayer.GetDamagePercentage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void StrongPlayerfDamagePercentage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);


        //    double actual = userPlayer.GetDamagePercentage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void WeakPlayerfDamagePercentage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);


        //    double actual = userPlayer.GetDamagePercentage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}



        //[Fact]
        //public void WeakPlayerfDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}



        //[Fact]
        //public void ModeratePlayerfDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Moderate);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}



        //[Fact]
        //public void StrongPlayerfDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}


        //[Fact]
        //public void StrongPlayerHP()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);


        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void NormalPlayerHP()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Moderate);


        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void WeakPlayerHP()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);


        //    double actual = userPlayer.HitProbability(userPlayer);
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void StrongPlayerDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void ModeratePlayerDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Moderate);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void WeakPlayerDamage()
        //{
        //    UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);


        //    double actual = userPlayer.GetDamage();
        //    double expected = 20;

        //    Assert.Equal(expected, actual);
        //}

        [Fact]
        public void StrongPlayerHpDamage()
        {
            UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Strong);


            double actual = userPlayer.Attack(userPlayer);
            double expected = 20;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ModerateHpPlayerDamage()
        {
            UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Normal);


            double actual = userPlayer.Attack(userPlayer);
            double expected = 20;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeakHpPlayerDamage()
        {
            UserPlayer userPlayer = PlayerFactory.GetPlayer(PlayerType.Weak);


            double actual = userPlayer.Attack(userPlayer);
            double expected = 20;

            Assert.Equal(expected, actual);
        }
    }
}