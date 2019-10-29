import os
InFolder = 'C:\\!TS\\Cooking'
OutFolder = 'C:\\!TS-out'

#folders = [(f.name, f.path) for f in os.scandir(InFolder) if f.is_dir()]

#for foldername, folder in folders:
folder = InFolder
files = [(f.name, f.path) for f in os.scandir(folder) if not f.is_dir() and (f.name.endswith(".html") or f.name.endswith(".htm") or f.name.endswith(".txt"))]
    #name, f = files[0]
for name, f in files: 
    #if len(files) > 1:
    #print(name)
    #for name, f in files:
    print("processing: " + f)    
    url = "file:///" + f.replace("\\", "/")
    outname = os.path.join(OutFolder, name) #foldername + ".html")
    #print(outname)
    os.system('single-file-ff.bat "{}" "{}"'.format(url, outname))