#  Notes


## Conflicts

+ For resolving conflicts, See https://developer.apple.com/library/content/documentation/General/Conceptual/iCloudDesignGuide/Chapters/DesigningForKey-ValueDataIniCloud.html#//apple_ref/doc/uid/TP40012094-CH7-SW1


## Limitations

+ iCloud key-value storage is 1 MB per user
+ The maximum number of keys you can specify is 1024
+ Size limit for each value associated with a key is 1 MB
+ The maximum length for a key string is 64 bytes using UTF8 encoding (The data size of your cumulative key strings does not count against your 1 MB total quota; rather, your key strings - which at maximum consume 64 KB - count against a user’s total iCloud allotment)


## Storing dictionary (graph)

+ `setDictionary:`
    + A dictionary whose contents can be stored in a property list format. In other words, the objects in the dictionary must be of the types `NSNumber`, `NSString`, `NSDate`, `NSData`, `NSArray`, or `NSDictionary`.
