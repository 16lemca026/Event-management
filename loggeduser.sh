echo "Enter the Username"
read name
if(who | grep -w $name)
then
echo "User is logged into the System"
else
echo "User is not logged into the System"
fi
