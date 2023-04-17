using System.Collections;

namespace Warehouse2.Sorters
{
    public class ClassifierDescSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var tx = x as ClassifierTreeNode;
            var ty = y as ClassifierTreeNode;

            if (tx.CurrentClassifier.SortCode != ty.CurrentClassifier.SortCode)
                return ty.CurrentClassifier.SortCode - tx.CurrentClassifier.SortCode;
            return string.Compare(ty.Text, tx.Text);
        }
    }
}
