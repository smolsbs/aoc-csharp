#!  /usr/bin/env python3

import argparse
import datetime
import os


ROOT_DIR = os.getcwd()

CSHARP_TEMPLATE = """using AdventOfCode.Runner.Attributes;

namespace Year__YEAR__.Day__DAY__;

[ProblemInfo(__YEAR__, __DAY__, "")]
public class Day__DAY__: Problem{
    public override void LoadInput() {
        return;
    }
    public override void CalculatePart1() {
        return;
    }
    public override void CalculatePart2() {
        return;
    }

}"""

def check_if_dir_exists(path):
    if not os.path.exists(path):
        os.makedirs(path)

def create(args):
    _path = f"{ROOT_DIR}/Years/year_{args['year']}/day{args['day']:02d}"
    check_if_dir_exists(_path)
    os.chdir(_path)
    open('input.txt', 'w', encoding='utf-8').close()
    fn = f"day{args['day']:02d}.cs"
    with open(fn, 'w',  encoding='utf-8') as fp:
        fp.write(CSHARP_TEMPLATE.replace('__YEAR__', str(args['year'])).replace('__DAY__', str(args['day'])))


def str2bool(v):
    if v.lower() in ('yes', 'true', 't', 'y', '1'):
        return True
    elif v.lower() in ('no', 'false', 'f', 'n', '0'):
        return False
    else:
        raise argparse.ArgumentTypeError('Boolean value expected.')


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--create', '-c',
                        action='store_true',
                        dest='create',
                        help='Creates a folder with the current day, or with a specific day, if -d is used')
    parser.add_argument('-d',
                        action='store',
                        dest='day',
                        type=int,
                        help='Sets the day to x.')
    parser.add_argument('-y', '--year',
                        action='store',
                        dest='year',
                        type=int,
                        help='Sets the year of the event. Defaults to the current year.')
    res = parser.parse_args()

    args = {'create': res.create,
            'curAdr': os.getcwd()}

    if res.day is None:
        args['day'] = int(datetime.datetime.today().day)
    else:
        args['day'] = res.day

    if res.year is None:
        args['year'] = int(datetime.datetime.today().year)
    else:
        args['year'] = res.year

    if args['create']:
        create(args)


if __name__ == '__main__':
    main()
