using System.IO.Compression;
using System.Runtime.CompilerServices;
using AdventOfCode.Runner.Attributes;
using Year2015.Day21;

namespace Year2016.Day04;

[ProblemInfo(2016, 4, "Security Through Obscurity")]
public class Day04 : Problem<int,int>{

    private string[] _inputData = Array.Empty<string>();
    private List<(string, int)> validRooms = new List<(string, int)>();
    public override void LoadInput() {
        _inputData = ReadInputLines();
        CheckIfValid();
    }
    public override void CalculatePart1() {
        Part1 = validRooms.Select(v => v.Item2).Sum();
        return;
    }
    public override void CalculatePart2() {

        foreach (var room in validRooms)
        {
            char[] shiftedRoom = new char[room.Item1.Length];
            for (var i = 0; i < room.Item1.Length; i++)
            {
                if (room.Item1[i] == '-')
                {
                    shiftedRoom[i] = ' ';
                    continue;
                }
                shiftedRoom[i] = (char)((room.Item1[i] - 'a' + (room.Item2 % 26)) % 26 + 'a');
            }

            var newName = string.Join(string.Empty, shiftedRoom);

            if (newName.Contains("northpole"))
            {
                Part2 = room.Item2;
                break;
            }
        }
        return;
    }


    private void CheckIfValid(){
        foreach ( var line in _inputData){
            string roomName = string.Join("", line.Split('[').First()[..^4]);
            string chars = roomName.Replace("-", string.Empty);
            int id = int.Parse(line.Split('-').Last()[..3]);
            string checksum = line.Split('[').Skip(1).First()[..5];

            Dictionary<char, int> aux = chars.GroupBy(c => c).ToDictionary(v => v.Key, v => v.Count());

            var top5 = string.Join("", CustomOrder(aux).Take(5));
        
            if(top5 == checksum)
                validRooms.Add((roomName, id));
        }
    }

    private List<char> CustomOrder(Dictionary<char, int> items){

        return items.OrderBy(item => item.Key)
                       .GroupBy(counter => counter.Value, counter => counter.Key)
                       .OrderByDescending(occurences => occurences.Key)
                       .SelectMany(occurence => occurence.Select(item => (occurence.Key, item)))
                       .Select(v => v.item)
                       .ToList();
    }

}