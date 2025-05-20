namespace App.Domain;

public class DecisionNode
{
    public int Id { get; set; }
    public int TreeId { get; set; }
    public string? Condition { get; set; } // Condition for branching
    public string? Formula { get; set; }  // Math formula for scoring (optional)
    public int? TrueBranchId { get; set; }
    public int? FalseBranchId { get; set; }

    // Navigation properties for EF
    public DecisionNode? TrueBranch { get; set; }
    public DecisionNode? FalseBranch { get; set; }

    // Check if the node is a leaf node
    public bool IsLeaf => TrueBranchId == null && FalseBranchId == null && Formula != null;

    public override string ToString() =>
       $"{Condition ?? "NULL"} ? {(TrueBranchId.HasValue ? "C" + TrueBranchId : "NULL")} : {(FalseBranchId.HasValue ? "C" + FalseBranchId : "NULL")} | {Formula ?? "NULL"}";
}
