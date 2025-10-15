using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static List<string> FindPairs(List<string> words)
    {
        var set = new HashSet<string>(words ?? new List<string>());
        var result = new List<string>();

        foreach (var word in words ?? Enumerable.Empty<string>())
        {
            var reversed = new string(word.Reverse().ToArray());
            if (set.Contains(reversed) && word != reversed)
            {
                var pair = $"{word} & {reversed}";
                var reversePair = $"{reversed} & {word}";
                if (!result.Contains(reversePair))
                    result.Add(pair);
            }
        }

        return result;
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degreeSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(filename)) return degreeSummary;

        var lines = File.ReadAllLines(filename);
        foreach (var line in lines.Skip(1)) // skip header
        {
            var parts = line.Split(',');
            if (parts.Length > 3)
            {
                var degree = parts[3].Trim();
                if (degree == "") degree = "Unknown";
                if (degreeSummary.ContainsKey(degree))
                    degreeSummary[degree]++;
                else
                    degreeSummary[degree] = 1;
            }
        }

        return degreeSummary;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1 == null || word2 == null) return false;

        word1 = new string(word1.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());
        word2 = new string(word2.ToLower().Where(c => !char.IsWhiteSpace(c)).ToArray());
        if (word1.Length != word2.Length) return false;

        var counts = new Dictionary<char, int>();
        foreach (var c in word1)
            counts[c] = counts.GetValueOrDefault(c) + 1;

        foreach (var c in word2)
        {
            if (!counts.ContainsKey(c)) return false;
            counts[c]--;
            if (counts[c] < 0) return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static async Task<List<string>> EarthquakeDailySummary()
    {
        const string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<FeatureCollection>(json);
        var results = new List<string>();
        if (data?.features == null) return results;

        foreach (var f in data.features)
        {
            var place = f?.properties?.place ?? "Unknown";
            var mag = f?.properties?.mag ?? 0.0;
            results.Add($"{place} - Mag {mag}");
        }
        return results;
    }

    public class FeatureCollection { public List<Feature> features { get; set; } }
    public class Feature { public Properties properties { get; set; } }
    public class Properties { public string place { get; set; } public double? mag { get; set; } }
}