using System.Diagnostics;
using Tero.Assets._scripts.Core.CoreComponents;
using Tero.Combat.KnockBack;
namespace Tero.CoreSystem
{

    public class KnockBackReceiver : BaseKnockback
    {
        // Use 'new' keyword to explicitly hide the inherited member and resolve CS0108
        private new void KnockBack(KnockBackData data)
        {
              KnockBack(data);
        }

        private new void KnockBackByGrenades(KnockBackData data)
        {
            KnockBackByGrenades(data);
        }
        private new void CheckKnockBack()
        {
            CheckKnockBack();
        }
        private new void CheckKnockBackGranade()
        {
            CheckKnockBackGranade();
        }
    }
}