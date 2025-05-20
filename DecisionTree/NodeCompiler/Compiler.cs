using App.Domain;
using System.Text.RegularExpressions;

namespace NodeCompiler;

internal class Compiler(List<DecisionNode> nodes)
{
    public List<DecisionNode> Compile(string input)
    {
        var lines = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
        int idCounter = 1;

        foreach (var line in lines)
        {
            var match = Regex.Match(line.Trim(), @"(NULL|.+?)\s*\?\s*(NULL|C\d+|\d+)\s*:\s*(NULL|C\d+|\d+)\s*\|\s*(.*)");
            if (match.Success)
            {
                nodes.Add(new DecisionNode
                {
                    Id = idCounter,
                    TreeId = 0,
                    Condition = match.Groups[1].Value.Trim() == "NULL" ? null : match.Groups[1].Value.Trim(),
                    TrueBranchId = ParseBranch(match.Groups[2].Value),
                    FalseBranchId = ParseBranch(match.Groups[3].Value),
                    Formula = match.Groups[4].Value.Trim() == "NULL" ? null : match.Groups[4].Value.Trim()
                });
                idCounter++;
            }
        }
        return nodes;
    }

    public void AssignBranchReferences()
    {
        foreach (var node in nodes)
        {
            node.TrueBranch = nodes.Find(n => n.Id == node.TrueBranchId);
            node.FalseBranch = nodes.Find(n => n.Id == node.FalseBranchId);
        }
    }

    static int? ParseBranch(string branchValue)
    {
        if (branchValue.StartsWith("C"))
            return int.Parse(branchValue.Substring(1)); // Convert 'C<number>' to integer index
        if (branchValue == "NULL")
            return null;
        return int.Parse(branchValue);
    }

    public string Decompile(List<DecisionNode> nodes)
    {
        var result = new List<string>();
        foreach (var node in nodes)
            result.Add(node.ToString());
        return string.Join("; ", result);
    }
}
