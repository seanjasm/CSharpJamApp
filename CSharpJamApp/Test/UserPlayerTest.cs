
using CSharpJamApp.Models;
using Xunit;

namespace CSharpJamApp.Test
{
    public class UserPlayerTest
    {
        [Fact]
        public void HitProbability()
        {
            double actual = new UserPlayer().HitProbability(null);
            double expected = 1;

            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void RandomIs20()
        //{
        //    int actual = new UserPlayer().GetRandomProbability(19, 20);
        //    int expected = 20;

        //    Assert.Equal(expected, actual);
        //}
    }
}