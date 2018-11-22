import java.io.*;
import java.util.*;

public class CountLines {
   public static void main (String[] args) {
      try {
         BufferedReader in = new BufferedReader (new FileReader("KeepMeCrazyMapAverage.txt"));
         int count = 0;
         while (in.readLine() != null) {
            count++;
         }
         in.close();
         System.out.println("count: " + count/2);
      } catch (IOException iox) {
         System.out.println("Error reading file");
      }
   }
}