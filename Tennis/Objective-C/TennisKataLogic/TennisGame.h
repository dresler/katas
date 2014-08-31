//
//  TennisGame.h
//  TennisGame
//
//  Created by Robert Dresler on 31/08/14.
//
//

#import <Foundation/Foundation.h>

@interface TennisGame : NSObject
-(NSString*) getScore;
-(void) addPoint:(NSInteger)playerIndex;
@end
