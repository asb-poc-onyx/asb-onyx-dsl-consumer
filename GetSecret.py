# Python script to fetch a secret and serve it up in a way the 
# Consumer and Producer applications understand.
import boto3
import sys
import json

client = boto3.client('secretsmanager')

response = client.get_secret_value(SecretId=sys.argv[1])
if None != response and 'SecretString' in response:
	with open('secrets.json','w') as fOut1:
		fOut1.write(response['SecretString'])
		fOut1.flush()