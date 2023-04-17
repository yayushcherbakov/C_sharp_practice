using System.Collections;

namespace Warehouse2.Sorters
{
    public class ClassifierAscSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var tx = x as ClassifierTreeNode;
            var ty = y as ClassifierTreeNode;

            if (tx.CurrentClassifier.SortCode != ty.CurrentClassifier.SortCode)
                return tx.CurrentClassifier.SortCode - ty.CurrentClassifier.SortCode;
            return string.Compare(tx.Text, ty.Text);
        }
    }
}
