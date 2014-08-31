//
//  TennisGame.m
//  TennisGame
//
//  Created by Robert Dresler on 31/08/14.
//
//

#import "TennisGame.h"

@interface TennisGame()
@property (nonatomic) int player1score;
@property (nonatomic) int player2score;
@end

@implementation TennisGame

NSArray *scoreNames;

-(NSString *) getScore {
    
    if ([self isWinner: _player1score opponentPoints:_player2score]) {
        return @"Player1Wins";
    }
    
    if ([self isWinner: _player2score opponentPoints:_player1score]) {
        return @"Player2Wins";
    }
    
    if (_player1score <= 2 || _player2score <= 2) {
        return [self getScoreWhenOnePlayerHasMaximumTwoPoints];
    }
    
    if ([self isDeuce]) {
        return @"Deuce";
    }
    
    if ([self hasAdvantage:_player1score opponentPoints:_player2score]) {
        return @"AdvantagePlayer1";
    }
    
    if ([self hasAdvantage:_player2score opponentPoints:_player1score]) {
        return @"AdvantagePlayer2";
    }
    
    @throw [NSException alloc];
}

-(bool) isWinner:(int)playerPoints opponentPoints:(int)opponentPoints {
    return (playerPoints == 4 && opponentPoints <= 2) || (playerPoints > 4 && playerPoints == opponentPoints + 2);
}

-(NSString *) getScoreWhenOnePlayerHasMaximumTwoPoints {
    return [NSString stringWithFormat:@"%@%@", [self getName:_player1score], [self getName:_player2score]];
}

-(bool) isDeuce {
    return _player1score >= 3 && _player1score == _player2score;
}

-(bool) hasAdvantage:(int)playerPoints opponentPoints:(int)opponentPoints {
    return (playerPoints >= 4 && playerPoints == opponentPoints + 1);
}

-(NSString *) getName:(int)points {
    return [scoreNames objectAtIndex:points];
}

-(void) addPoint:(NSInteger)playerIndex {
    _player1score += playerIndex == 1 ? 1 : 0;
    _player2score += playerIndex == 2 ? 1 : 0;
}

- (id)init {
    scoreNames = @[@"Love", @"Fifteen", @"Thirty", @"Forty"];
    return [super init];
}
@end
