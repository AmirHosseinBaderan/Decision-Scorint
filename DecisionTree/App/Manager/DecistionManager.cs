using App.Context;
using App.Domain;

namespace App.Manager;

public class DecisionTreeManager
{
    public void CreateAndSaveTree()
    {
        using (var db = new DecisionTreeDbContext())
        {
            // Create nodes
            var root = new DecisionNode
            {
                Condition = "score > 50",
                TrueBranch = new DecisionNode
                {
                    Condition = "score > 80",
                    TrueBranch = new DecisionNode { Score = 100 },
                    FalseBranch = new DecisionNode { Score = 70 }
                },
                FalseBranch = new DecisionNode
                {
                    Condition = "score > 30",
                    TrueBranch = new DecisionNode { Score = 50 },
                    FalseBranch = new DecisionNode { Score = 20 }
                }
            };

            // Save the tree
            db.DecisionNodes.Add(root);
            db.SaveChanges();
        }
    }
}
