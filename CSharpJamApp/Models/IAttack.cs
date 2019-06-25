using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpJamApp.Models
{
    public interface IAttack
    {
        double Attack(UserPlayer player);
        double HitProbability(UserPlayer player);
    }
}
