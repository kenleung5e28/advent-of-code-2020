using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

long EvaluateExpression(string expr)
{
    var stack = new Stack<(char, long)>();
    foreach (char c in expr)
    {  
        if (c >= '0' && c <= '9')
        {
            if (stack.Count == 0 || stack.Peek().Item1 == '(')
            {
                stack.Push((' ', c - '0'));
            }
            else
            {
                char op = stack.Pop().Item1;
                long op1 = c - '0';
                long op2 = stack.Pop().Item2;
                long answer = op == '*' ? op1 * op2 : op1 + op2;
                stack.Push((' ', answer));
            }
        }
        else if (c == '(' || c == '+' || c == '*')
        {
            stack.Push((c, -1));
        }
        else if (c == ')')
        {
            long op2 = stack.Pop().Item2;
            stack.Pop();
            if (stack.Count == 0 || stack.Peek().Item1 == '(')
            {
                stack.Push((' ', op2));
            }
            else
            {
                char op = stack.Pop().Item1;
                long op1 = stack.Pop().Item2;
                long answer = op == '*' ? op1 * op2 : op1 + op2;
                stack.Push((' ', answer));
            }
        }
    }
    return stack.Pop().Item2;
}

long EvaluateExpression2(string expr)
{
    var sb = new StringBuilder();
    var stack = new Stack<char>();
    foreach (char c in expr)
    {
        if ((c >= '0' && c <= '9'))
        {
            sb.Append(c);
        }
        else if (c == '+')
        {
            if (stack.Count == 0 || stack.Peek() == '(' || stack.Peek() == '*')
            {
                stack.Push(c);
            }
            else
            {
                while (stack.Count > 0 && stack.Peek() == '+')
                {
                    sb.Append(stack.Pop());
                }
                stack.Push(c);
            }
        }
        else if (c == '*')
        {
            if (stack.Count == 0 || stack.Peek() == '(')
            {
                stack.Push(c);
            }
            else
            {
                while (stack.Count > 0 && (stack.Peek() == '+' || stack.Peek() == '*'))
                {
                    sb.Append(stack.Pop());
                }
                stack.Push(c);
            }
        }
        else if (c == '(')
        {
            stack.Push(c);
        }
        else if (c == ')')
        {
            while (stack.Peek() != '(')
            {
                sb.Append(stack.Pop());
            }
            stack.Pop();
        }
    }
    while (stack.Count > 0)
    {
        sb.Append(stack.Pop());
    }
    string postfix = sb.ToString();
    var evalstack = new Stack<long>();
    foreach (char c in postfix)
    {
        if (c >= '0' || 'c' <= '9')
        {
            evalstack.Push(Convert.ToInt64(c - '0'));
        }
        else
        {
            long op1 = evalstack.Pop();
            long op2 = evalstack.Pop();
            evalstack.Push(c == '+' ? op1 + op2 : op1 * op2);
        }
    }
    return evalstack.Pop();
}

long Part1(string[] lines)
{
    long sum = 0;
    foreach (string line in lines)
    {
        long value = EvaluateExpression(Regex.Replace(line, @"\s+", ""));
        // Console.WriteLine(value);
        sum += value;
    }
    return sum;
}

long Part2(string[] lines)
{
    long sum = 0;
    foreach (string line in lines)
    {
        long value = EvaluateExpression2(Regex.Replace(line, @"\s+", ""));
        // Console.WriteLine(value);
        sum += value;
    }
    return sum;
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 1: {0}", Part2(lines));