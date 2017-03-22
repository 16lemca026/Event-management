echo "Enter the string:"
read str
echo
len=`echo $str | wc -c`
len=`expr $len - 1`
a=1
b=`expr $len / 2`
while test $a -le $b
do
i=`echo $str | cut -c $a`
j=`echo $str | cut -c $len`
if test $i != $j
then
echo "Given string is not palindrome"
exit
fi
a=`expr $a + 1`
len=`expr $len - 1`
done
echo "Given string is palendrome"
