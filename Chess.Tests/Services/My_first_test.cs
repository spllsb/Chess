using System.Threading.Tasks;
using NUnit.Framework;

namespace Chess.Tests.Services
{
    [TestFixture]
    public class My_first_test
    {
        [Test]
        public async Task given_number_should_be_not_ten()
        {
            var number = await GetNumber();
            var result = IsTen(number);

            Assert.IsFalse(result,$"Number '{number}' isn't 10");
        }
    
        private async Task <int> GetNumber()
            => await Task.FromResult(11);
        private bool IsTen(int number)
        {
            return number == 10 ? true:false;
        }
    }
}