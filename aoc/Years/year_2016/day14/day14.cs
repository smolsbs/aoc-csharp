using System.Data;
using AdventOfCode.Runner.Attributes;

namespace Year2016.Day14;

[ProblemInfo(2016, 14, "One-Time Pad")]
public class Day14: Problem<int,int>{
    private string _inputData = "";
    private MD5 hasher = MD5.Create();
    private List<int> validKeys = new List<int>();
    private struct possibleKey{
        public int position;
        public char c;
        public bool isValid;
    }

    public override void LoadInput() {
        _inputData = ReadInputText();
    }

    public override void CalculatePart1() {
        Part1 = IHateYouTopaz();
    }

    public override void CalculatePart2() {
        validKeys.Clear();
        Part2 = IHateYouTopaz(2017);
    }

    private string HashString(string v, int iter = 1){
        byte[] prepare = Array.Empty<byte>();
        string stringPrepare = v;
        MD5 h = MD5.Create();
        for (var idx = 0; idx < iter; idx++){
            prepare = Encoding.ASCII.GetBytes(stringPrepare);
            stringPrepare = BitConverter.ToString(h.ComputeHash(prepare)).Replace("-", "").ToLower();
        }
        return stringPrepare;
    }

    private int IHateYouTopaz(int nIter=1){
        int i = 0;
        bool run = true;
        char? ret;
        List<possibleKey> keyHolder = new List<possibleKey>();
        while (run){

            string hashed = HashString($"{_inputData}{i}", nIter);
            // check for new possible keys
            ret = isKey(hashed, 3);
            if (ret is char v){
                possibleKey aux = new possibleKey{position = i, c=v, isValid=true};
                keyHolder.Add(aux);
            }

            List<char> ret_5 = isKey5(hashed);
            if (ret_5.Count == 0){
                i++;
                continue;
            }

            // on possible keys, check further into them
            for (var idx = 0; idx < keyHolder.Count; idx++){
                if (keyHolder[idx].position == i)
                    continue;

                if (keyHolder[idx].position + 1000 < i ){
                    var thing = keyHolder[idx];
                    thing.isValid = false;
                    keyHolder[idx] = thing;
                    continue;
                }

                if (!keyHolder[idx].isValid)
                    continue;
                
                if (ret_5.Any(c => c == keyHolder[idx].c)){
                    validKeys.Add(keyHolder[idx].position);
                    Console.WriteLine(validKeys.Count);
                    var thing = keyHolder[idx];
                    thing.isValid = false;
                    keyHolder[idx] = thing;
                }
            }

            var aux3 = keyHolder.RemoveAll(c => c.isValid == false);

            if (validKeys.Count >= 64){
                break;
            }
            i++;
        }
        return validKeys[63];
    }

  
    private char? isKey(string hash, int size, char? v=null){
        var aux = Enumerable.Range(0, hash.Length-size).Select(c => hash[c..(c+size)]).FirstOrDefault(c => c.All(x => x == (v ?? c[0]) ));
        return aux?[0];

    }

    private List<char> isKey5(string hash) {
        var aux = Enumerable.Range(0, hash.Length-5).Select(c => hash[c..(c+5)]).Where(c => c.All(x => x == (c[0]))).Select(v => v[0]).ToList();
        return aux;
    }

}
