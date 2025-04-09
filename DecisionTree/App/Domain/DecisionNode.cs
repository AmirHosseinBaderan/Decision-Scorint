namespace App.Domain;

public class DecisionNode
{
    public int Id { get; set; } // Primary Key
    public int TreeId { get; set; } // Foreign Key for tree identification

    public string? Condition { get; set; } // Logical condition (e.g., "score > 50")
    public int? Score { get; set; } // Score for leaf nodes
    public int? TrueBranchId { get; set; } // True branch node ID
    public int? FalseBranchId { get; set; } // False branch node ID

    // Navigation properties for EF
    public DecisionNode? TrueBranch { get; set; }
    public DecisionNode? FalseBranch { get; set; }
}
