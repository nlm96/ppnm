using static System.Console;
class main{
    static void Main(string[] args){
        var list = new genlist<double[]>();
        char[] delimiters = {' ', '\t'};
        var options = System.StringSplitOptions.RemoveEmptyEntries;

        for (string line = ReadLine(); line!=null ; line = ReadLine()){
            var words = line.Split(delimiters,options);
            int n = words.Length;
            var numbers = new double[n];
            for (int i=0 ; i<n ; i++ ){
                numbers[i] = double.Parse(words[i]);
            }
            list.add(numbers);
        }
        for (int i = 0 ; i<list.size ; i++){
            var numbers = list[i];
            foreach (var number in numbers){
                Write($"{number : 0.00e+00;-0.00e+00}");
            }
            WriteLine();
        }
    }
}