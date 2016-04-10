# Queens
Comparing threaded vs non threaded 8 queens for C#
 
Measuring the time it takes can be done in different ways, either after setting threads and everything upp or including the setup in the measuring.

Current implementation is with setup. This means that small sizes (<=8) will probably result in (smartersolver) threading will be worse than the not threaded version.
