using AdventOfCode.Runner.Attributes;

namespace Year2016.Day19;

[ProblemInfo(2016, 19, "An Elephant Named Joseph")]
public class Day19: Problem<int,int>{

    private int Size = 5;

    public override void LoadInput() {
        Size = int.Parse(ReadInputText());
    }

    public override void CalculatePart1() {
        Elf rootElf = new Elf { id = 1 };
        Elf elf = rootElf;

        for (int idx = 2; idx < Size+1; idx++){
            elf.next = (idx == Size) ? rootElf : new Elf{ id = idx + 2, prev = elf };
            elf = elf.next;
        }

        rootElf.prev = elf;
        Elf target = rootElf.next;
        

        while(elf.next != elf){
            target.prev.next = target.next;
            target.next.prev = target.prev;

        }
        Part1 = elf.id;
    }
    public override void CalculatePart2() {
        Elf rootElf = new Elf { id = 1 };
        Elf elf = rootElf;
        Elf across = null;

        for (int idx = 1; idx < Size+1; idx++){
            elf.next = (idx == Size) ? rootElf : new Elf{ id = idx + 1, prev = elf };
            elf = elf.next;
            if ( idx == Size / 2)
                across = elf;
        }

        rootElf.prev = elf;

        int size = Size;

        while (elf.next != elf) {

            across.prev.next = across.next;
            across.next!.prev = across.prev;

            across = size % 2 == 1 ? across.next.next : across.next;
            size--;

            elf = elf.next!;
        }
        Part2 = elf.id;
    }

}

public class Elf{
    public int id { get; set; }
    public Elf prev { get; set; }
    public Elf next { get; set; }

}
