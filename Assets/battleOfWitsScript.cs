using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class battleOfWitsScript : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable Lectern;
    public TextMesh[] LecternTexts; //0 = Brown, 1 = Orange, 2 = Azure
    public GameObject TV;
    public Material[] TVMats; //0 = Orange, 1 = Azure, 2 = Black
    public GameObject DotGrid;
    public KMSelectable[] Dots;
    public Material[] DotMats; //Same as TV
    public GameObject[] DotObjs;
    public SpriteRenderer[] Places; //Chair, Stool, Air
    public Sprite[] Things; //Nothing, Empty chair, Dude in chair, Robot chair, Dude stool, Robot stool, Speech bubble

    string modifiedSN = "";
    int snShift = 0;
    int caesarShift = 0;
    string base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string[] perms = { "123456", "123465", "123546", "123564", "123645", "123654", "124356", "124365", "124536", "124563", "124635", "124653", "125346", "125364", "125436", "125463", "125634", "125643", "126345", "126354", "126435", "126453", "126534", "126543", "132456", "132465", "132546", "132564", "132645", "132654", "134256", "134265", "134526", "134562", "134625", "134652", "135246", "135264", "135426", "135462", "135624", "135642", "136245", "136254", "136425", "136452", "136524", "136542", "142356", "142365", "142536", "142563", "142635", "142653", "143256", "143265", "143526", "143562", "143625", "143652", "145236", "145263", "145326", "145362", "145623", "145632", "146235", "146253", "146325", "146352", "146523", "146532", "152346", "152364", "152436", "152463", "152634", "152643", "153246", "153264", "153426", "153462", "153624", "153642", "154236", "154263", "154326", "154362", "154623", "154632", "156234", "156243", "156324", "156342", "156423", "156432", "162345", "162354", "162435", "162453", "162534", "162543", "163245", "163254", "163425", "163452", "163524", "163542", "164235", "164253", "164325", "164352", "164523", "164532", "165234", "165243", "165324", "165342", "165423", "165432", "213456", "213465", "213546", "213564", "213645", "213654", "214356", "214365", "214536", "214563", "214635", "214653", "215346", "215364", "215436", "215463", "215634", "215643", "216345", "216354", "216435", "216453", "216534", "216543", "231456", "231465", "231546", "231564", "231645", "231654", "234156", "234165", "234516", "234561", "234615", "234651", "235146", "235164", "235416", "235461", "235614", "235641", "236145", "236154", "236415", "236451", "236514", "236541", "241356", "241365", "241536", "241563", "241635", "241653", "243156", "243165", "243516", "243561", "243615", "243651", "245136", "245163", "245316", "245361", "245613", "245631", "246135", "246153", "246315", "246351", "246513", "246531", "251346", "251364", "251436", "251463", "251634", "251643", "253146", "253164", "253416", "253461", "253614", "253641", "254136", "254163", "254316", "254361", "254613", "254631", "256134", "256143", "256314", "256341", "256413", "256431", "261345", "261354", "261435", "261453", "261534", "261543", "263145", "263154", "263415", "263451", "263514", "263541", "264135", "264153", "264315", "264351", "264513", "264531", "265134", "265143", "265314", "265341", "265413", "265431", "312456", "312465", "312546", "312564", "312645", "312654", "314256", "314265", "314526", "314562", "314625", "314652", "315246", "315264", "315426", "315462", "315624", "315642", "316245", "316254", "316425", "316452", "316524", "316542", "321456", "321465", "321546", "321564", "321645", "321654", "324156", "324165", "324516", "324561", "324615", "324651", "325146", "325164", "325416", "325461", "325614", "325641", "326145", "326154", "326415", "326451", "326514", "326541", "341256", "341265", "341526", "341562", "341625", "341652", "342156", "342165", "342516", "342561", "342615", "342651", "345126", "345162", "345216", "345261", "345612", "345621", "346125", "346152", "346215", "346251", "346512", "346521", "351246", "351264", "351426", "351462", "351624", "351642", "352146", "352164", "352416", "352461", "352614", "352641", "354126", "354162", "354216", "354261", "354612", "354621", "356124", "356142", "356214", "356241", "356412", "356421", "361245", "361254", "361425", "361452", "361524", "361542", "362145", "362154", "362415", "362451", "362514", "362541", "364125", "364152", "364215", "364251", "364512", "364521", "365124", "365142", "365214", "365241", "365412", "365421", "412356", "412365", "412536", "412563", "412635", "412653", "413256", "413265", "413526", "413562", "413625", "413652", "415236", "415263", "415326", "415362", "415623", "415632", "416235", "416253", "416325", "416352", "416523", "416532", "421356", "421365", "421536", "421563", "421635", "421653", "423156", "423165", "423516", "423561", "423615", "423651", "425136", "425163", "425316", "425361", "425613", "425631", "426135", "426153", "426315", "426351", "426513", "426531", "431256", "431265", "431526", "431562", "431625", "431652", "432156", "432165", "432516", "432561", "432615", "432651", "435126", "435162", "435216", "435261", "435612", "435621", "436125", "436152", "436215", "436251", "436512", "436521", "451236", "451263", "451326", "451362", "451623", "451632", "452136", "452163", "452316", "452361", "452613", "452631", "453126", "453162", "453216", "453261", "453612", "453621", "456123", "456132", "456213", "456231", "456312", "456321", "461235", "461253", "461325", "461352", "461523", "461532", "462135", "462153", "462315", "462351", "462513", "462531", "463125", "463152", "463215", "463251", "463512", "463521", "465123", "465132", "465213", "465231", "465312", "465321", "512346", "512364", "512436", "512463", "512634", "512643", "513246", "513264", "513426", "513462", "513624", "513642", "514236", "514263", "514326", "514362", "514623", "514632", "516234", "516243", "516324", "516342", "516423", "516432", "521346", "521364", "521436", "521463", "521634", "521643", "523146", "523164", "523416", "523461", "523614", "523641", "524136", "524163", "524316", "524361", "524613", "524631", "526134", "526143", "526314", "526341", "526413", "526431", "531246", "531264", "531426", "531462", "531624", "531642", "532146", "532164", "532416", "532461", "532614", "532641", "534126", "534162", "534216", "534261", "534612", "534621", "536124", "536142", "536214", "536241", "536412", "536421", "541236", "541263", "541326", "541362", "541623", "541632", "542136", "542163", "542316", "542361", "542613", "542631", "543126", "543162", "543216", "543261", "543612", "543621", "546123", "546132", "546213", "546231", "546312", "546321", "561234", "561243", "561324", "561342", "561423", "561432", "562134", "562143", "562314", "562341", "562413", "562431", "563124", "563142", "563214", "563241", "563412", "563421", "564123", "564132", "564213", "564231", "564312", "564321", "612345", "612354", "612435", "612453", "612534", "612543", "613245", "613254", "613425", "613452", "613524", "613542", "614235", "614253", "614325", "614352", "614523", "614532", "615234", "615243", "615324", "615342", "615423", "615432", "621345", "621354", "621435", "621453", "621534", "621543", "623145", "623154", "623415", "623451", "623514", "623541", "624135", "624153", "624315", "624351", "624513", "624531", "625134", "625143", "625314", "625341", "625413", "625431", "631245", "631254", "631425", "631452", "631524", "631542", "632145", "632154", "632415", "632451", "632514", "632541", "634125", "634152", "634215", "634251", "634512", "634521", "635124", "635142", "635214", "635241", "635412", "635421", "641235", "641253", "641325", "641352", "641523", "641532", "642135", "642153", "642315", "642351", "642513", "642531", "643125", "643152", "643215", "643251", "643512", "643521", "645123", "645132", "645213", "645231", "645312", "645321", "651234", "651243", "651324", "651342", "651423", "651432", "652134", "652143", "652314", "652341", "652413", "652431", "653124", "653142", "653214", "653241", "653412", "653421", "654123", "654132", "654213", "654231", "654312", "654321" };
    string permOrange = "";
    string permAzure = "";
    string seqOrange = "";
    string seqAzure = "";
    string usedCharacters = "";
    string[] NATO = { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliett", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-Ray", "Yankee", "Zulu" };
    string[] fullGrids = { "LS3QJO2BKN49TMRAPIC1EX85FUZ6HW0DGVY7", "S50J8XOWZ1HFVMQ4EL3DTC6UGN2RAYP9KIB7", "SLIM15BRO20DGT34XYZ6WUF7HPJ8CAKE9QVN", "1UZ4W3Y05V2X7T6AQRMNIDFEGCHJLKPBOS98", "AWBXCYDZEFGHI0J1K2L3M4N5O6P7Q8R9STUV", "XKOY9E4P1BWJI8FNVZQUA50G7DHMT3SC62LR", "0369CFILORUX147ADGJMPSVY258BEHKNQTWZ", "QZ5NOREW2BU7C48VX9DLYHJMKS3PT0AI1FG6", "0I1J2K3L4M5N6O7P8Q9RASBTCUDVEWFXGYHZ", "09IR1AJS2BKT3CLU4DMV5ENW6FOX7GPY8HQZ", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", "PDZSIRO4K8T21M6GLUQV93A0YNHEFC57WBJX", "12345QWERTYUIOPASDFGHJKLZXCVBNM67890", "KIO68QU9ZCPDSJMEVRAT1X53B427HLG0YFWN", "CYXD7SVI0NUTLJMQOHERF45G2986P31KWZAB", "BMVF31QZ0Y4SXJ5GIW7H6A2EPRLNTKUDC98O", "MWC509QI31NOSJB2FHUDXZ6PLV7TYK8G4ERA", "A1B2C3D4E5F6G7H8I9JK0LMNOPQRSTUVWXYZ", "0182HP3BDJ4GMT95KVFZX6RCQLAY7EWONUSI", "ABCDE1FGHIJ2LMNOP3QRSTU4VWXYZ567890K", "19IR0AJS2BKT3CLU4DMV5ENW6FOX7GPY8HQZ", "ABCIVW259DFHNOXZ78GJKMPST34ELQRUY016", "1D8USBKRCN6MA9V4QXJTZ5LY27OGIEW3HP0F", "AQBRCSDTEU0FV1GW2HX3IY4JZ5K6L7M8N9OP", "AE7VOM8TYG0DK4CW2UHNXP5JS6L13QBRI9FZ" };
    string chosenNATO = "";
    string chosenGrid = "";
    bool validPuzzle = true;
    int generationAttempts = 0;
    bool debating = false;
    int chosenSide = 0;
    bool failed = false;
    int lastSelected = -1;
    string characterPath = "";
    int elapsed = 0;
    bool failSafe;
    private int[][] _possiblePaths;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;

        for (int x = 0; x < Dots.Count(); x++)
        {
            int Number = x;
            Dots[Number].OnInteract += delegate
            {
                DotPress(Number);
                return false;
            };
        }

        Lectern.OnInteract += delegate () { LecternPress(); return false; };

    }

    // Use this for initialization
    void Start()
    {
        DotGrid.SetActive(false);
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        modifiedSN = Bomb.GetSerialNumber(); //Get the serial number
        Debug.LogFormat("[Battle of Wits #{0}] SN starts at {1}", moduleId, modifiedSN);

        snShift = UnityEngine.Random.Range(0, 11) - 5; //Shift it left or right
        int soWeCanGetItBackLater = snShift;
        if (snShift < 0)
        {
            snShift *= -1;
            while (snShift != 0)
            {
                modifiedSN = ("" + modifiedSN[1] + modifiedSN[2] + modifiedSN[3] + modifiedSN[4] + modifiedSN[5] + modifiedSN[0]);
                snShift -= 1;
            }
        }
        else if (snShift > 0)
        {
            while (snShift != 0)
            {
                modifiedSN = ("" + modifiedSN[5] + modifiedSN[0] + modifiedSN[1] + modifiedSN[2] + modifiedSN[3] + modifiedSN[4]);
                snShift -= 1;
            }
        }
        snShift = soWeCanGetItBackLater;
        Debug.LogFormat("[Battle of Wits #{0}] After shifting the SN by {1} we get {2}", moduleId, snShift, modifiedSN);

        caesarShift = UnityEngine.Random.Range(0, 71) - 35; //Caesar shift it using base 36
        soWeCanGetItBackLater = caesarShift;
        string newSN = "";
        for (int c = 0; c < 6; c++)
        {
            newSN += base36[(base36.IndexOf(modifiedSN[c]) + caesarShift + 36) % 36];
        }
        modifiedSN = newSN;
        caesarShift = soWeCanGetItBackLater;
        Debug.LogFormat("[Battle of Wits #{0}] After caesar shifting by {1} we get {2}", moduleId, caesarShift, modifiedSN);

        permOrange = perms[UnityEngine.Random.Range(0, 720)]; //Rearrange the characters in two different ways
        permAzure = perms[UnityEngine.Random.Range(0, 720)];
        for (int l = 0; l < 6; l++)
        {
            switch (permOrange[l])
            {
                case '1': seqOrange += modifiedSN[0]; break;
                case '2': seqOrange += modifiedSN[1]; break;
                case '3': seqOrange += modifiedSN[2]; break;
                case '4': seqOrange += modifiedSN[3]; break;
                case '5': seqOrange += modifiedSN[4]; break;
                case '6': seqOrange += modifiedSN[5]; break;
            }
            switch (permAzure[l])
            {
                case '1': seqAzure += modifiedSN[0]; break;
                case '2': seqAzure += modifiedSN[1]; break;
                case '3': seqAzure += modifiedSN[2]; break;
                case '4': seqAzure += modifiedSN[3]; break;
                case '5': seqAzure += modifiedSN[4]; break;
                case '6': seqAzure += modifiedSN[5]; break;
            }
        }
        Debug.LogFormat("[Battle of Wits #{0}] Orange's permuatation is {1} so now orange's sequence is {2}", moduleId, permOrange, seqOrange);
        Debug.LogFormat("[Battle of Wits #{0}] Azure's permuatation is {1} so now azure's sequence is {2}", moduleId, permAzure, seqAzure);

        for (int q = 0; q < 2; q++)
        { //Remove duplicate characters, only include earliest occurance
            usedCharacters = "";
            string k = "";
            if (q == 0) { k = seqOrange; } else { k = seqAzure; }
            for (int m = 0; m < 6; m++)
            {
                if (usedCharacters.IndexOf(k[m]) == -1)
                {
                    usedCharacters += k[m];
                }
            }
            if (q == 0) { seqOrange = usedCharacters; } else { seqAzure = usedCharacters; }
        }
        if (usedCharacters.Length != 6)
        {
            Debug.LogFormat("[Battle of Wits #{0}] After removing duplicates orange and azure's sequences are {1} and {2} respectively", moduleId, seqOrange, seqAzure);
        }

        TryAgain:
        generationAttempts += 1;
        int number = UnityEngine.Random.Range(0, 25); //Choose a grid to use
        chosenNATO = NATO[number];
        chosenGrid = fullGrids[number];
        if (chosenNATO == "Charlie" && (UnityEngine.Random.Range(0, 2) == 0))
        {
            chosenNATO = "Kilo";
        }

        _possiblePaths = new[] { FindValidPath(chosenGrid, seqOrange), FindValidPath(chosenGrid, seqAzure) }; //See if there is a solution for both sides
        if (_possiblePaths.Any(i => i == null) && generationAttempts < 100)
        {
            goto TryAgain; //If not, choose a different grid
        }
        else if (_possiblePaths.Any(i => i == null) && generationAttempts == 100)
        {
            failSafe = true;
            LecternTexts[0].text = "SEND BLAN\n OR QUINN\n A LOG. PRESS\nME TWICE TO\nSOLVE!"; //If it doesn't become valid after 100 attempts, give up!
            Debug.LogFormat("[Battle of Wits #{0}] 100 generation attempts passed, initiating unicorn...", moduleId);
        }
        else
        {
            LecternTexts[0].text = chosenNATO + "\n" + snShift + " " + caesarShift; //Put required data on the lectern
            LecternTexts[1].text = permOrange;
            LecternTexts[2].text = permAzure;
            Debug.LogFormat("[Battle of Wits #{0}] Your grid is {1}", moduleId, chosenNATO);
            Debug.LogFormat("<Battle of Wits #{0}> Generated after {1} attempts", moduleId, generationAttempts);
        }
        Debug.LogFormat("[Battle of Wits #{0}] Possible orange path: {1}.", moduleId, _possiblePaths[0].Select(i => GetCoord(i)).Join(", "));
        Debug.LogFormat("[Battle of Wits #{0}] Possible azure path: {1}.", moduleId, _possiblePaths[1].Select(i => GetCoord(i)).Join(", "));
    }

    string GetCoord(int coord)
    {
        return "ABCDEF"[coord % 6].ToString() + (coord / 6 + 1).ToString();
    }

    void DotPress(int d)
    {
        for (int p = 0; p < 36; p++)
        {
            if (d == p)
            {
                int good = 0;
                if (lastSelected == -1) { lastSelected = p; characterPath += chosenGrid[p]; good = 1; }
                if (5 < lastSelected) { if (lastSelected - 6 == p) { good = 1; } }
                if (lastSelected % 6 != 0) { if (lastSelected - 1 == p) { good = 1; } }
                if (lastSelected % 6 != 5) { if (lastSelected + 1 == p) { good = 1; } }
                if (lastSelected < 30) { if (lastSelected + 6 == p) { good = 1; } }
                if (good == 0)
                {
                    Debug.LogFormat("[Battle of Wits #{0}] Invalid path configuration, strike!", moduleId);
                    Fail();
                }
                else
                {
                    lastSelected = p; characterPath += chosenGrid[p];
                    DotObjs[p].GetComponent<MeshRenderer>().material = DotMats[chosenSide];
                    Audio.PlaySoundAtTransform("bet-" + "abcdefghijklmnopqrstuvwxyz".PickRandom(), transform);
                }
            }
        }
    }

    void LecternPress()
    {
        Lectern.AddInteractionPunch();
        if (moduleSolved) { return; }
        if (failSafe)
        {
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
            return;
        }
        if (!debating)
        {
            debating = true;
            DotGrid.SetActive(true);
            chosenSide = UnityEngine.Random.Range(0, 2); //0=Orange, 1=Azure
            TV.GetComponent<MeshRenderer>().material = TVMats[chosenSide];
            Debug.LogFormat("[Battle of Wits #{0}] Chosen side is {1}", moduleId, (chosenSide == 0) ? "orange" : "azure");
            Places[0].sprite = Things[1];
            if (!failed)
            {
                Places[1].sprite = Things[4];
            }
            else
            {
                Places[1].sprite = Things[5];
            }
            Places[2].sprite = Things[6];
            StartCoroutine(Timer());
        }
        else
        {
            debating = false;
            if (CheckValidity())
            {
                Debug.LogFormat("[Battle of Wits #{0}] Path is valid, module solved!", moduleId);
                GetComponent<KMBombModule>().HandlePass();
                moduleSolved = true;
                TV.GetComponent<MeshRenderer>().material = TVMats[2];
                Places[2].sprite = Things[0];
                Places[1].sprite = Things[0];
                DotGrid.SetActive(false);
                Audio.PlaySoundAtTransform("clap", transform);
            }
            else
            {
                Debug.LogFormat("[Battle of Wits #{0}] Path is invalid, strike!", moduleId);
                Fail();
            }
        }
    }

    bool CheckValidity()
    {
        string important = "";
        if (characterPath == "") { return false; }
        for (int a = 1; a < characterPath.Length; a++)
        {
            if (usedCharacters.IndexOf(characterPath[a]) != -1)
            {
                important += characterPath[a];
            }
        }
        Debug.LogFormat("[Battle of Wits #{0}] Full: {1} / Important: {2} ", moduleId, characterPath.Substring(1), important);
        if (chosenSide == 0)
        {
            return important == seqOrange;
        }
        else
        {
            return important == seqAzure;
        }
    }

    IEnumerator Timer()
    {
        while (elapsed < 30 && debating)
        {
            yield return new WaitForSeconds(1f);
            elapsed += 1;
        }
        if (debating)
        {
            Debug.LogFormat("[Battle of Wits #{0}] Failed to finish within 30 seconds, strike!", moduleId);
            Fail();
        }
        else
        {
            elapsed = 0;
        }
    }

    void Fail()
    {
        GetComponent<KMBombModule>().HandleStrike();
        failed = true;
        DotGrid.SetActive(false);
        TV.GetComponent<MeshRenderer>().material = TVMats[2];
        Places[0].sprite = Things[3];
        Places[1].sprite = Things[0];
        Places[2].sprite = Things[0];
        for (int g = 0; g < 36; g++)
        {
            DotObjs[g].GetComponent<MeshRenderer>().material = DotMats[2];
        }
        lastSelected = -1;
        characterPath = "";
        debating = false;
        elapsed = 0;
    }

    //twitch plays
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} start [Start the debate.] | !{0} press b4 urdlld [Press the buttons on the grid, then press the buttons in the directions given. e.g. b4 urdlld = b4, b3, c3, c4, b4, a4, a5] | !{0} submit [Submit the answer on the grid.]";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToLowerInvariant();
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(command, @"^\s*start\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (debating)
            {
                yield return "sendtochaterror The debate is already happening. Command ignored.";
                yield break;
            }
            Lectern.OnInteract();
        }

        if (Regex.IsMatch(command, @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (!debating)
            {
                yield return "sendtochaterror The debate is currently not happening. Command ignored.";
                yield break;
            }
            Lectern.OnInteract();
        }

        var m = Regex.Match(command, @"^\s*(?:press\s+)(?<col>[abcdef])(?<row>[123456])(?<dirs>\s+[urdl ]+)?\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (m.Success)
        {
            if (!debating)
            {
                yield return "sendtochaterror The debate is currently not happening. Command ignored.";
                yield break;
            }
            int col = "abcdef".IndexOf(m.Groups["col"].Value);
            int row = "123456".IndexOf(m.Groups["row"].Value);
            int pos = row * 6 + col;
            var list = new List<int>();
            if (m.Groups["dirs"].Success)
            {
                var dirString = m.Groups["dirs"].Value;
                for (int i = 0; i < dirString.Length; i++)
                {
                    int ix = "urdl ".IndexOf(dirString[i]);
                    if (ix == 0) row--;
                    if (ix == 1) col++;
                    if (ix == 2) row++;
                    if (ix == 3) col--;
                    if (col < 0 || row < 0 || col > 5 || row > 5)
                        yield break;
                    pos = row * 6 + col;
                    list.Add(pos);
                }
            }
            yield return null;
            for (int i = 0; i < list.Count; i++)
            {
                Dots[list[i]].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        if (debating)
        {
            // Quinn Wuest got lazy around here.
            StopAllCoroutines();
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
            yield break;
        }
        var seqStrings = new string[] { seqOrange, seqAzure };
        var paths = new int[2][];
        var grid = chosenGrid;
        for (int i = 0; i < seqStrings.Length; i++)
            paths[i] = _possiblePaths[i];
        Lectern.OnInteract();
        for (int i = 0; i < paths[chosenSide].Length; i++)
        {
            Dots[paths[chosenSide][i]].OnInteract();
            yield return new WaitForSeconds(0.2f);
        }
        Lectern.OnInteract();
        yield break;
    }

    struct QueueItem
    {
        public int Cell;
        public int Parent;
        public QueueItem(int cell, int parent)
        {
            Cell = cell;
            Parent = parent;
        }
    }

    private int[] FindValidPath(string grid, string seqString)
    {
        var path = new List<int>();
        var seq = new int[seqString.Length];
        for (int j = 0; j < seq.Length; j++)
            seq[j] = grid.IndexOf(seqString[j]);
        int curPos = 0;
        for (int pathNum = 0; pathNum < seqString.Length; pathNum++)
        {
            var innerPath = new List<int>();
            int goal = grid.IndexOf(seqString[pathNum]);
            if (pathNum == 0)
            {
                curPos = goal;
                innerPath.Add(curPos);
            }
            var q = new Queue<QueueItem>();
            var visited = new Dictionary<int, QueueItem>();
            q.Enqueue(new QueueItem(curPos, -1));
            while (q.Count > 0)
            {
                var qi = q.Dequeue();
                if (visited.ContainsKey(qi.Cell))
                    continue;
                visited[qi.Cell] = qi;
                if (qi.Cell == goal)
                    goto done;
                if (qi.Cell % 6 < 5 && !(seq.Contains(qi.Cell + 1) && qi.Cell + 1 != goal))
                    q.Enqueue(new QueueItem(qi.Cell + 1, qi.Cell));
                if (qi.Cell % 6 > 0 && !(seq.Contains(qi.Cell - 1) && qi.Cell - 1 != goal))
                    q.Enqueue(new QueueItem(qi.Cell - 1, qi.Cell));
                if (qi.Cell / 6 < 5 && !(seq.Contains(qi.Cell + 6) && qi.Cell + 6 != goal))
                    q.Enqueue(new QueueItem(qi.Cell + 6, qi.Cell));
                if (qi.Cell / 6 > 0 && !(seq.Contains(qi.Cell - 6) && qi.Cell - 6 != goal))
                    q.Enqueue(new QueueItem(qi.Cell - 6, qi.Cell));
            }
            return null;
            done:
            var r = goal;
            while (true)
            {

                var nr = visited[r];
                if (nr.Parent == -1)
                    break;
                innerPath.Add(nr.Cell);
                r = nr.Parent;
            }
            curPos = goal;
            innerPath.Reverse();
            path.AddRange(innerPath);
        }
        return path.ToArray();
    }
}