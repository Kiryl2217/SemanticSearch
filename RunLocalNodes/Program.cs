using System.Diagnostics;

Console.Write("Какое количество узлов запустить: ");
int countNode = int.Parse(Console.ReadLine());

for (var nodeNumber = 1; nodeNumber <= countNode; nodeNumber++)
{
    Process.Start("../../../../SemanticNode\\bin\\Debug\\net8.0\\SemanticNode.exe", nodeNumber.ToString() + " y");
}