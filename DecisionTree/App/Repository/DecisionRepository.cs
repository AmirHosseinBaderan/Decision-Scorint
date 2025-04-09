using App.Context;
using App.Domain;

namespace App.Repository;

public class DecisionTreeRepository : IDecisionTreeRepository
{
    private readonly DecisionTreeDbContext _context;

    public DecisionTreeRepository(DecisionTreeDbContext context)
    {
        _context = context;
    }

    public void AddTree(DecisionNode root)
    {
        _context.DecisionNodes.Add(root);
        _context.SaveChanges();
    }

    public DecisionNode GetNode(int nodeId)
    {
        return _context.DecisionNodes.Find(nodeId);
    }

    public List<DecisionNode> GetAllNodes(int treeId)
    {
        return _context.DecisionNodes.Where(n => n.TreeId == treeId).ToList();
    }

    public void UpdateNodeCondition(int nodeId, string newCondition)
    {
        var node = _context.DecisionNodes.Find(nodeId);
        if (node != null)
        {
            node.Condition = newCondition;
            _context.SaveChanges();
        }
    }

    public DecisionNode GetRootNode(int treeId)
    {
        return _context.DecisionNodes.FirstOrDefault(n => n.TreeId == treeId && n.TrueBranchId == null && n.FalseBranchId == null);
    }
}
