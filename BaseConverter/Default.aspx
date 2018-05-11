<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaseConverter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Base Converter</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            Select a numeral system:
            <br />
            <asp:DropDownList ID="baseDropDownList" runat="server">
                <asp:ListItem Selected="True" Value="1" Text="Base 1 (Unary)"></asp:ListItem>
                <asp:ListItem Value="2" Text="Base 2 (Binary)"></asp:ListItem>
                <asp:ListItem Value="3" Text="Base 3 (Ternary)"></asp:ListItem>
                <asp:ListItem Value="4" Text="Base 4 (Quaternary)"></asp:ListItem>
                <asp:ListItem Value="5" Text="Base 5 (Quinary)"></asp:ListItem>
                <asp:ListItem Value="6" Text="Base 6 (Senary)"></asp:ListItem>
                <asp:ListItem Value="7" Text="Base 7 (Septenary)"></asp:ListItem>
                <asp:ListItem Value="8" Text="Base 8 (Octal)"></asp:ListItem>
                <asp:ListItem Value="9" Text="Base 9 (Nonary)"></asp:ListItem>
                <asp:ListItem Value="10" Text="Base 10 (Decimal)"></asp:ListItem>
                <asp:ListItem Value="11" Text="Base 11 (Undecimal)"></asp:ListItem>
                <asp:ListItem Value="12" Text="Base 12 (Duodecimal)"></asp:ListItem>
                <asp:ListItem Value="13" Text="Base 13 (Tridecimal)"></asp:ListItem>
                <asp:ListItem Value="14" Text="Base 14 (Tetradecimal)"></asp:ListItem>
                <asp:ListItem Value="15" Text="Base 15 (Pentadecimal)"></asp:ListItem>
                <asp:ListItem Value="16" Text="Base 16 (Hexadecimal)"></asp:ListItem>
                <asp:ListItem Value="17" Text="Base 17 (Septemdecimal)"></asp:ListItem>
                <asp:ListItem Value="18" Text="Base 18 (Octodecimal)"></asp:ListItem>
                <asp:ListItem Value="19" Text="Base 19 (Nonadecimal)"></asp:ListItem>
                <asp:ListItem Value="20" Text="Base 20 (Vigesimal)"></asp:ListItem>
                <asp:ListItem Value="21" Text="Base 21 (Unvigesimal)"></asp:ListItem>
                <asp:ListItem Value="22" Text="Base 22 (Duovigesimal)"></asp:ListItem>
                <asp:ListItem Value="23" Text="Base 23 (Trivigesimal)"></asp:ListItem>
                <asp:ListItem Value="24" Text="Base 24 (Tetravigesimal)"></asp:ListItem>
                <asp:ListItem Value="25" Text="Base 25 (Pentavigesimal)"></asp:ListItem>
                <asp:ListItem Value="26" Text="Base 26 (Hexavigesimal)"></asp:ListItem>
                <asp:ListItem Value="27" Text="Base 27 (Septemvigesimal)"></asp:ListItem>
                <asp:ListItem Value="28" Text="Base 28 (Octovigesimal)"></asp:ListItem>
                <asp:ListItem Value="29" Text="Base 29 (Nonavigesimal)"></asp:ListItem>
                <asp:ListItem Value="30" Text="Base 30 (Trigesimal)"></asp:ListItem>
                <asp:ListItem Value="31" Text="Base 31 (Untrigesimal)"></asp:ListItem>
                <asp:ListItem Value="32" Text="Base 32 (Duotrigesimal)"></asp:ListItem>
                <asp:ListItem Value="33" Text="Base 33 (Tritrigesimal)"></asp:ListItem>
                <asp:ListItem Value="34" Text="Base 34 (Tetratrigesimal)"></asp:ListItem>
                <asp:ListItem Value="35" Text="Base 35 (Pentatrigesimal)"></asp:ListItem>
                <asp:ListItem Value="36" Text="Base 36 (Hexatrigesimal)"></asp:ListItem>
                <asp:ListItem Value="37" Text="Base 37 (Septemtrigesimal)"></asp:ListItem>
                <asp:ListItem Value="38" Text="Base 38 (Octotrigesimal)"></asp:ListItem>
                <asp:ListItem Value="39" Text="Base 39 (Nonatrigesimal)"></asp:ListItem>
                <asp:ListItem Value="40" Text="Base 40 (Tetragesimal)"></asp:ListItem>
                <asp:ListItem Value="41" Text="Base 41 (Untetragesimal)"></asp:ListItem>
                <asp:ListItem Value="42" Text="Base 42 (Duotetragesimal)"></asp:ListItem>
                <asp:ListItem Value="43" Text="Base 43 (Tritetragesimal)"></asp:ListItem>
                <asp:ListItem Value="44" Text="Base 44 (Tetratetragesimal)"></asp:ListItem>
                <asp:ListItem Value="45" Text="Base 45 (Pentatetragesimal)"></asp:ListItem>
                <asp:ListItem Value="46" Text="Base 46 (Hexatetragesimal)"></asp:ListItem>
                <asp:ListItem Value="47" Text="Base 47 (Septemtetragesimal)"></asp:ListItem>
                <asp:ListItem Value="48" Text="Base 48 (Octotetragesimal)"></asp:ListItem>
                <asp:ListItem Value="49" Text="Base 49 (Nonatetragesimal)"></asp:ListItem>
                <asp:ListItem Value="50" Text="Base 50 (Pentagesimal)"></asp:ListItem>
                <asp:ListItem Value="51" Text="Base 51 (Unpentagesimal)"></asp:ListItem>
                <asp:ListItem Value="52" Text="Base 52 (Duopentagesimal)"></asp:ListItem>
                <asp:ListItem Value="53" Text="Base 53 (Tripentagesimal)"></asp:ListItem>
                <asp:ListItem Value="54" Text="Base 54 (Tetrapentagesimal)"></asp:ListItem>
                <asp:ListItem Value="55" Text="Base 55 (Pentapentagesimal)"></asp:ListItem>
                <asp:ListItem Value="56" Text="Base 56 (Hexapentagesimal)"></asp:ListItem>
                <asp:ListItem Value="57" Text="Base 57 (Septempentagesimal)"></asp:ListItem>
                <asp:ListItem Value="58" Text="Base 58 (Octopentagesimal)"></asp:ListItem>
                <asp:ListItem Value="59" Text="Base 59 (Nonapentagesimal)"></asp:ListItem>
                <asp:ListItem Value="60" Text="Base 60 (Hexagesimal)"></asp:ListItem>
                <asp:ListItem Value="61" Text="Base 61 (Unhexagesimal)"></asp:ListItem>
                <asp:ListItem Value="62" Text="Base 62 (Duohexagesimal)"></asp:ListItem>
                <asp:ListItem Value="63" Text="Base 63 (Trihexagesimal)"></asp:ListItem>
                <asp:ListItem Value="64" Text="Base 64 (Tetrahexagesimal)"></asp:ListItem>
                <asp:ListItem Value="65" Text="Base 65 (Pentahexagesimal)"></asp:ListItem>
                <asp:ListItem Value="66" Text="Base 66 (Hexahexagesimal)"></asp:ListItem>
                <asp:ListItem Value="67" Text="Base 67 (Septemhexagesimal)"></asp:ListItem>
                <asp:ListItem Value="68" Text="Base 68 (Octohexagesimal)"></asp:ListItem>
                <asp:ListItem Value="69" Text="Base 69 (Nonahexagesimal)"></asp:ListItem>
                <asp:ListItem Value="70" Text="Base 70 (Septemgesimal)"></asp:ListItem>
                <asp:ListItem Value="71" Text="Base 71 (Unseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="72" Text="Base 72 (Duoseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="73" Text="Base 73 (Triseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="74" Text="Base 74 (Tetraseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="75" Text="Base 75 (Pentaseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="76" Text="Base 76 (Hexaseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="77" Text="Base 77 (Septemseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="78" Text="Base 78 (Octoseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="79" Text="Base 79 (Nonaseptemgesimal)"></asp:ListItem>
                <asp:ListItem Value="80" Text="Base 80 (Octogesimal)"></asp:ListItem>
                <asp:ListItem Value="81" Text="Base 81 (Unoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="82" Text="Base 82 (Duooctogesimal)"></asp:ListItem>
                <asp:ListItem Value="83" Text="Base 83 (Trioctogesimal)"></asp:ListItem>
                <asp:ListItem Value="84" Text="Base 84 (Tetraoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="85" Text="Base 85 (Pentaoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="86" Text="Base 86 (Hexaoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="87" Text="Base 87 (Septemoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="88" Text="Base 88 (Octooctogesimal)"></asp:ListItem>
                <asp:ListItem Value="89" Text="Base 89 (Nonaoctogesimal)"></asp:ListItem>
                <asp:ListItem Value="90" Text="Base 90 (Nonagesimal)"></asp:ListItem>
                <asp:ListItem Value="91" Text="Base 91 (Unnonagesimal)"></asp:ListItem>
                <asp:ListItem Value="92" Text="Base 92 (Duononagesimal)"></asp:ListItem>
                <asp:ListItem Value="93" Text="Base 93 (Trinonagesimal)"></asp:ListItem>
                <asp:ListItem Value="94" Text="Base 94 (Tetranonagesimal)"></asp:ListItem>
                <asp:ListItem Value="95" Text="Base 95 (Pentanonagesimal)"></asp:ListItem>
                <asp:ListItem Value="96" Text="Base 96 (Hexanonagesimal)"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            Input a number in your chosen numeral system:<br />
            <asp:TextBox ID="inputTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="convertButton" runat="server" Text="Convert to Decimal" OnClick="convertButton_Click" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            NOTATION (Digit=Decimal Value):<br />
            <br />
            <asp:Table ID="Table1" runat="server" GridLines="Both">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">0=0</asp:TableCell>
                    <asp:TableCell runat="server">1=1</asp:TableCell>
                    <asp:TableCell runat="server">2=2</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">3=3</asp:TableCell>
                    <asp:TableCell runat="server">4=4</asp:TableCell>
                    <asp:TableCell runat="server">5=5</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">6=6</asp:TableCell>
                    <asp:TableCell runat="server">7=7</asp:TableCell>
                    <asp:TableCell runat="server">8=8</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">                    
                    <asp:TableCell runat="server">9=9</asp:TableCell>
                    <asp:TableCell runat="server">A=10</asp:TableCell>
                    <asp:TableCell runat="server">B=11</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">C=12</asp:TableCell>
                    <asp:TableCell runat="server">D=13</asp:TableCell>
                    <asp:TableCell runat="server">E=14</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">F=15</asp:TableCell>
                    <asp:TableCell runat="server">G=16</asp:TableCell>
                    <asp:TableCell runat="server">H=17</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">I=18</asp:TableCell>
                    <asp:TableCell runat="server">J=19</asp:TableCell>
                    <asp:TableCell runat="server">K=20</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">L=21</asp:TableCell>
                    <asp:TableCell runat="server">M=22</asp:TableCell>
                    <asp:TableCell runat="server">N=23</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">O=24</asp:TableCell>
                    <asp:TableCell runat="server">P=25</asp:TableCell>
                    <asp:TableCell runat="server">Q=26</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">R=27</asp:TableCell>
                    <asp:TableCell runat="server">S=28</asp:TableCell>
                    <asp:TableCell runat="server">T=29</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">U=30</asp:TableCell>
                    <asp:TableCell runat="server">V=31</asp:TableCell>
                    <asp:TableCell runat="server">W=32</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">X=33</asp:TableCell>
                    <asp:TableCell runat="server">Y=34</asp:TableCell>
                    <asp:TableCell runat="server">Z=35</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">a=36</asp:TableCell>
                    <asp:TableCell runat="server">b=37</asp:TableCell>
                    <asp:TableCell runat="server">c=38</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">d=39</asp:TableCell>
                    <asp:TableCell runat="server">e=40</asp:TableCell>
                    <asp:TableCell runat="server">f=41</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">g=42</asp:TableCell>
                    <asp:TableCell runat="server">h=43</asp:TableCell>
                    <asp:TableCell runat="server">i=44</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">j=45</asp:TableCell>
                    <asp:TableCell runat="server">k=46</asp:TableCell>
                    <asp:TableCell runat="server">l=47</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">m=48</asp:TableCell>
                    <asp:TableCell runat="server">n=49</asp:TableCell>
                    <asp:TableCell runat="server">o=50</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">p=51</asp:TableCell>
                    <asp:TableCell runat="server">q=52</asp:TableCell>
                    <asp:TableCell runat="server">r=53</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">s=54</asp:TableCell>
                    <asp:TableCell runat="server">t=55</asp:TableCell>
                    <asp:TableCell runat="server">u=56</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">v=57</asp:TableCell>
                    <asp:TableCell runat="server">w=58</asp:TableCell>
                    <asp:TableCell runat="server">x=59</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">y=60</asp:TableCell>
                    <asp:TableCell runat="server">z=61</asp:TableCell>
                    <asp:TableCell runat="server">/=62</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">:=63</asp:TableCell>
                    <asp:TableCell runat="server">;=64</asp:TableCell>
                    <asp:TableCell runat="server">(=65</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">)=66</asp:TableCell>
                    <asp:TableCell runat="server">$=67</asp:TableCell>
                    <asp:TableCell runat="server">&=68</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">@=69</asp:TableCell>
                    <asp:TableCell runat="server">"=70</asp:TableCell>
                    <asp:TableCell runat="server">,=71</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">?=72</asp:TableCell>
                    <asp:TableCell runat="server">!=73</asp:TableCell>
                    <asp:TableCell runat="server">'=74</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">[=75</asp:TableCell>
                    <asp:TableCell runat="server">]=76</asp:TableCell>
                    <asp:TableCell runat="server">{=77</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">}=78</asp:TableCell>
                    <asp:TableCell runat="server">#=79</asp:TableCell>
                    <asp:TableCell runat="server">%=80</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">^=81</asp:TableCell>
                    <asp:TableCell runat="server">*=82</asp:TableCell>
                    <asp:TableCell runat="server">+=83</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">==84</asp:TableCell>
                    <asp:TableCell runat="server">_=85</asp:TableCell>
                    <asp:TableCell runat="server">\=86</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">|=87</asp:TableCell>
                    <asp:TableCell runat="server">~=88</asp:TableCell>
                    <asp:TableCell runat="server"><=89</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">>=90</asp:TableCell>
                    <asp:TableCell runat="server">€=91</asp:TableCell>
                    <asp:TableCell runat="server">£=92</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">¥=93</asp:TableCell>
                    <asp:TableCell runat="server">•=94</asp:TableCell>
                    <asp:TableCell runat="server">&nbsp;=95</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
