
using System.Collections.Concurrent;

namespace Sorts_searches
{
    class program
    {

        #region Searchs 
        public static int binarySearch(List<int> elements, int x)
        {
            int start = 0;
            int stop = elements.Count - 1;
            int middle = (int)((start + stop) / 2);
            while (elements[middle] != x && start < stop)
            {
                if (x < elements[middle])
                {
                    stop = middle - 1;
                }
                else
                {
                    start = middle + 1;
                }
                middle = (int)((start + stop) / 2);
            }
            return (elements[middle] != x) ? -1 : middle;
        }

        public static int interpolationSearch(int[] arr, int lenght, int x)
        {
            int low = 0;
            int high = lenght - 1;
            int pos = 0;
            while (low <= high && x >= arr[low] && x <= arr[high])
            {
                if (low == high)
                {
                    if (arr[low] == x)
                    {
                        return low;
                    }
                    else
                    {
                        return -1;
                    }
                }
                pos = low + (high - low) / (arr[high] - arr[low]) * (x - arr[low]);
                if (arr[pos] == x)
                {
                    return pos;
                }
                if (arr[pos] < x)
                {
                    low = pos + 1;
                }
                else
                {
                    high = pos - 1;
                }
            }
            return pos;
        }
        #endregion

        #region Sorts

        public static int[] selectionSort(int[] arr)
        {
            int lenght = arr.Length;
            for (int i = 0; i < lenght; i++)
            {
                int mainIndex = i;
                for (int j = 0; j < lenght; j++)
                {
                    if (arr[j] > arr[mainIndex])
                    {
                        mainIndex = j;
                        int temporary = arr[i];
                        arr[i] = arr[mainIndex];
                        arr[mainIndex] = temporary;
                    }
                }
            }
            return arr;
        }

        public static int[] bubbleSort(int[] arr)
        {
            int lenght = arr.Length;
            for (int i = lenght - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (arr[j - 1] > arr[j])
                    {
                        int temp = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            return arr;
        }

        #region quick sort
        public static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (arr[left] == arr[right])
                    {
                        return right;
                    }
                    return left;
                }
            }
        }
        public static int[] quickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                if (pivot > 1)
                {
                    quickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    quickSort(arr, pivot + 1, right);
                }
            }
            return arr;
        }
        #endregion

        #region Merge sort
        public static int[] merge(int[] left, int[] right)
        {
            int resultLenght = left.Length + right.Length;
            int[] result = new int[resultLenght];
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                if (indexLeft < left.Length && indexRight < right.Length)
                {

                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
        public static int[] mergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];

            if (array.Length <= 1)
            {
                return array;
            }
            int midpoint = (int)(array.Length / 2);
            left = new int[midpoint];

            if (array.Length % 2 == 0)
            {
                right = new int[midpoint];
            }
            else
            {
                right = new int[midpoint + 1];
            }

            for (int i = 0; i < midpoint; i++)
            {
                left[i] = array[i];
            }
            int count = 0;
            for (int i = midpoint; i < array.Length; i++)
            {
                right[count] = array[i];
                count++;
            }
            left = mergeSort(left);
            right = mergeSort(right);
            result = merge(left, right);
            return result;
        }
        #endregion

        public static int[] insertionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int value = arr[i];
                int flag = 0;
                for (int j = i - 1; j > -1 && flag != 1;)
                {
                    if (value < arr[j])
                    {
                        arr[j + 1] = arr[j];
                        j--;
                        arr[j + 1] = value;
                    }
                    else flag = 1;
                }

            }
            return arr;
        }


        public static int[] radixSort(int[] data)
        {
            int[] temp = new int[data.Length];
            for (int shift = 31; shift > -1; shift--)
            {
                int j = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    bool move = (data[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                    {
                        data[i - j] = data[i];
                    }
                    else
                    {
                        temp[j++] = data[i];
                    }
                }
                Array.Copy(temp, 0, data, data.Length - j, j);
            }
            return data;
        }


        #region Heap sort

        static void swap(int[] arr, int element1, int element2)
        {
            int swap = arr[element1];
            arr[element1] = arr[element2];
            arr[element2] = swap;
        }

        static void heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && arr[left] > arr[largest])
            {
                largest = left;
            }
            if (right < n && arr[right] > arr[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                swap(arr, i, largest);
                heapify(arr, n, largest);
            }
        }

        public static int[] heapSort(int[] arr)
        {
            int lenght = arr.Length;
            for (int i = lenght / 2 - 1; i >= 0; i--)
            {
                heapify(arr, lenght, i);
            }
            for (int i = lenght - 1; i >= 0; i--)
            {
                swap(arr, 0, i);
                heapify(arr, i, 0);
            }
            return arr;
        }


        #endregion


        public static int[] shellSort(int[] arr)
        {
            int lenght = arr.Length - 1;
            for (int gap = (int)(lenght / 2); gap > 0; gap = (int)(gap / 2))
            {
                for (int i = 0; i + gap < arr.Length; i++)
                {
                    int temp = arr[i];
                    if (arr[i + gap] < arr[i])
                    {
                        arr[i] = arr[i + gap];
                        arr[i + gap] = temp;
                    }
                }
            }
            return arr;
        }

        #endregion

        static void Main(string[] args)
        {

            int[] arr = new int[6] { 1, 23, 300, 46, 44, 200 };
            int[] arr1 = shellSort(arr);
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.Write(arr1[i] + "  ");
            }
            Console.ReadLine();
        }
    }
}
