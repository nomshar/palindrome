This algorithm is trying to search all palindromes in the input query.
The main idea is to apply methods from geometry to find substrings and its mirrors (mirros for abc is cba).
All symbols in the input strings are splitted into pairs of points (First and Second). For each pair algorithm calc a slope value.
Than starting from the first pair algorithm is looking for a vertex, storing points pairs into a temprorary list.
When algorithm meets a pair with a slope which is different by its sign (Math.Sign()), it assumes the edge is found.
After this it is trying to find a mirror vertex and in case of success builds a palindrome.
For the result the algorithm returns a list of all palindromes that it could find.

Test cases:
abcddcbah
abcdbacefeh
abcedffdecba
aa
aba
abcedffdecbakllkgg
