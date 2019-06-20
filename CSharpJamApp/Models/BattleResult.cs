namespace CSharpJamApp.Models
{
    public class BattleResult
    {
        public string Result { get; set; }
        
        public BattleResult()
        {

        }

        public BattleResult(string result)
        {
            Result = result;
        }
    }
}