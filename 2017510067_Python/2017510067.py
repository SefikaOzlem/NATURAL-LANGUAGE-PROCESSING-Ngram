#!/usr/bin/env python
# -*- coding: utf-8 -*-
import re
import io
import time


result=""         #temp the n gram format array value returned from the ngram_create function 
ngram_dict=dict() #the dictionary containing n gram format string parts and their count values

def ngram_create(words, n): # the function that converts the words in the split text to n gram format 
    ngrams = []             # array holding string parts in n gram format

    for i in range(0, len(words)-n+1):
        ngram = ' '.join(words[i:i + n]) # Combining words up to n values
        ngrams.append(ngram)
    return ngrams
   
file_name=input("Enter file name (ppp.txt) : ")
n_val=int(input("Enter n value : "))
file_path="Novel-Samples/"+file_name
try:
    start = time.time() #when the program starts running after the file name is requested from the user
    #"C:/Users/Lenovo/Desktop/4.sınıf/4408 INTRODUCTION TO NATURAL LANGUAGE PROCESSING/Novel-Samples/"+ file_name  (file path on my computer)
    file = io.open(file_path,"r", encoding="cp1254",errors='ignore') # The file path was created by combining the Novel-Samples folder with the text name entered by the user.
    line = file.read().replace("\n", " ") # remove null line 
    file.close()

    text=re.sub(r'[^\w\s]', '', line)     # remove punctuations 
    text=" ".join(text.split())
    text=text.replace("I","ı").replace("İ","i").lower()
    text=text.split()
    result=ngram_create(text, n_val)

    #In this loop, string parts in n gram format are added to the dictionary together with value. 
    for i in range(len(result)):
        if result[i] in ngram_dict.keys():
            ngram_dict[result[i]]=ngram_dict[result[i]]+1
        else:
            ngram_dict[result[i]]=1

    count=1 #Check counter used to print most repeated only 50 items
    sorted_d = dict( sorted(ngram_dict.items(), key=lambda item: item[1],reverse=True))#A dictionary that holds the value of the items in the n gram dictionary in descending order.  

    for k,v in sorted_d.items():
        if count<=50:
            print (count,":",k,"-",v)
            count=count+1
            
    end = time.time()           # program end time
    elapsed_time = end - start  #time elapsed while the program is running
    elapsed_time_milliSeconds = elapsed_time*1000
    print("Elapsed time is ",elapsed_time_milliSeconds,"ms")
    
except Exception:
     print('File Not Found or Mistyped')    

input()

