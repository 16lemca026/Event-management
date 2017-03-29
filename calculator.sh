res= 0
c=1
while [ $c != 0 ]
do
echo "Enter the First Number"
read a
echo "Enter the Second Number"
read b
echo "Please select the operation"
echo "Click 1 for addition"
echo "Click 2 for subtraction"
echo "Click 3 for Multiplication"
echo "Click 4 for Division"

read op

case $op in
   1) res=` expr $a + $b`
	echo "The sum of $a and $b is $res" 
   ;;
   2) res=` expr $a - $b`
	echo "The Difference of $a and $b is $res" 
   ;;
   3) res=` expr $a \* $b`
	echo "The Product of $a and $b is $res"  
   ;;
4)  res=` expr $a / $b`
	echo "The result of $a and $b is $res" 
   ;;
esac
echo "Press 1 for continue. 0 for Exit"
read c
done
