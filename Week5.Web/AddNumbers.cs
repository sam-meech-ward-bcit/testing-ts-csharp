public class AddNumbers {
    private int[] _numbers { get; set; }

    public AddNumbers(int[] numbers) {
        _numbers = numbers;
    }

    public int Sum() {
        var sum = 0;
        foreach (var number in _numbers) {
            sum += number;
        }
        return sum;
    }
}