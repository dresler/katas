//
//  TennisKataLogicTests.m
//  TennisKataLogicTests
//
//  Created by Robert Dresler on 31/08/14.
//
//

#import <XCTest/XCTest.h>
#import <KNMParametrizedTests/KNMParametrizedTests.h>
#import "TennisGame.h"

@interface TennisKataLogicTests : XCTestCase

@end

@implementation TennisKataLogicTests

KNMParametersFor(testGetScoreWhenPlayersAddPoints,
  @[
  @[@[], @"LoveLove"],
  @[@[@1], @"FifteenLove" ],
  @[@[@2], @"LoveFifteen" ],
  @[@[@1, @2], @"FifteenFifteen" ],
  @[@[@1, @1], @"ThirtyLove" ],
  @[@[@1, @1, @2], @"ThirtyFifteen" ],
  @[@[@1, @1, @1], @"FortyLove" ],
  @[@[@1, @1, @1, @2], @"FortyFifteen" ],
  @[@[@1, @1, @1, @2, @2], @"FortyThirty" ],
  @[@[@1, @1, @1, @1], @"Player1Wins" ],
  @[@[@2, @2, @2, @2], @"Player2Wins" ],
  @[@[@1, @2, @1, @2, @1, @2], @"Deuce" ],
  @[@[@1, @2, @1, @2, @1, @2, @1], @"AdvantagePlayer1" ],
  @[@[@1, @2, @1, @2, @1, @2, @2], @"AdvantagePlayer2" ],
  @[@[@1, @2, @1, @2, @1, @2, @2, @2], @"Player2Wins" ]
  ])
- (void)testGetScoreWhenPlayersAddPoints:(NSArray *)testData
{
    NSArray *pointsPlayerIndexes = testData[0];
    NSString *expectedResult = testData[1];

    TennisGame *game = [[TennisGame alloc] init];
    
    [self addPoints:game pointsPlayerIndexes:pointsPlayerIndexes];

    XCTAssertEqualObjects(expectedResult, game.getScore);
}

- (void)addPoints:(TennisGame *)game pointsPlayerIndexes:(NSArray *)pointsPlayerIndexes
{
    for (int i = 0; i < [pointsPlayerIndexes count]; i++) {
        int pointPlayerIndex = [(NSNumber *)[pointsPlayerIndexes objectAtIndex:i] intValue];
        [game addPoint:pointPlayerIndex];
    }
}

@end
